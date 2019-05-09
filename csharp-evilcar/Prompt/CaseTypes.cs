using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.Prompt.CaseTypes
{
	internal abstract class Base
	{
		public Base(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
		public virtual ReturnValue Init(Base parentsCase = null)
		{
			ParentsCase = parentsCase;
			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess, this);
		}
		public IEnumerable<Base> SubCases = null;
		public Base ParentsCase = null;
		internal CaseTypeFlags flags = CaseTypeFlags.Main;
		public string CaseName = null;
		public string AskForParameters = null;
		public string Help = null;
		public virtual string Syntax { get; set; }
		public virtual bool EmptyIsLegal => false;

		public IList<Base> CasePath()
		{

			IList<Base> Path = new List<Base>();
			Base CurrentCase = this;
			do
			{
				Path.Add(CurrentCase);
				CurrentCase = CurrentCase.ParentsCase;
			}
			while (CurrentCase != null);
			return Path.Reverse().ToList();
		}

		public string GetCaseName => CaseName ?? "CaseName-undefined";
		public virtual string GetAskForParameters => AskForParameters ?? string.Concat(CaseName, "-AskFor-undefined");
		public string GetHelp => Help ?? string.Concat(CaseName, "-help-undefined");
		public virtual string GetSyntax => null;
		public virtual ReturnValue CheckParameterLength(string[] inputArray) => ReturnValue.GetValue(ErrorCodeFlags.IsSuccess, this);
		public virtual ReturnValue Execute(ref string[] parameters)
		{
			if (parameters.Length == 0)
			{
				_ = InputOutput.Print(GetAskForParameters);
				(parameters, _) = InputOutput.GetInput();
			}
			return CheckParameterLength(parameters);

		}

	}
	internal class Selection : Base
	{
		public Selection(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
		public override ReturnValue Init(Base ParentsCase = null)
		{
			this.ParentsCase = ParentsCase;
			foreach (Base Subcase in SubCases)
			{
				_ = Subcase.Init(this);
			}
			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess, this);
		}
		public IEnumerable<Base> SubCases = null;
		public override string Syntax => string.Join("\n", from c in SubCases select c.Syntax) + "\n";
		public override ReturnValue CheckParameterLength(string[] inputArray) => inputArray.Count() > 0 ? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess, this) : ReturnValue.GetValue(ErrorCodeFlags.IsEmpty, this);
		public override ReturnValue Execute(ref string[] parameters)
		{
			if (ReturnValue.Execute(out ReturnValue code, base.Execute(ref parameters)) == ErrorCodeFlags.IsError || code == ErrorCodeFlags.IsEmpty)
			{
				return code;
			}
			string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
			parameters = parameters.Skip(1).ToArray();

			Base selectedCase = ( from s in SubCases
								  where s.CaseName == selection
								  select s ).SingleOrDefault();

			return UserMessages.General.HelpSymbols.Contains(selection)
				? ReturnValue.GetValue(ErrorCodeFlags.IsHelpNeeded, this)
				: selectedCase == default 
					? ReturnValue.GetValue(ErrorCodeFlags.IsWrongArgument, this) 
					: selectedCase.Execute(ref parameters);
		}


	}
	internal class Command : Base
	{

		public Command(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
		public int[] ParameterLength = null;
		public override ReturnValue CheckParameterLength(string[] inputArray) => ParameterLength.Contains(inputArray.Count()) ? ReturnValue.GetValue(ErrorCodeFlags.IsSuccess, this) : ReturnValue.GetValue(ErrorCodeFlags.IsWrongParameterLength, this);
		public Func<IEnumerable<string>, ReturnValue> SubFunction = null;

		public override ReturnValue Execute(ref string[] parameters)
		{
			if (AskForParameters != null)
			{
				ReturnValue code = base.Execute(ref parameters);
				if (code.Flags.HasFlag(ErrorCodeFlags.IsError))
				{
					return code;
				}
			}

			if (parameters.Any(s => UserMessages.General.HelpSymbols.Contains(s)))
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsHelpNeeded, this);
			}
			else if (CheckParameterLength(parameters).Flags.HasFlag(ErrorCodeFlags.IsSuccess))
			{
				return SubFunction is null 
					? ReturnValue.GetValue(ErrorCodeFlags.IsCommandFunctionUndefined, this) 
					: ReturnValue.Execute(SubFunction(parameters), this);
			}
			else
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsCommandAbort, this);
			}
		}
	}
	internal class Default : Base
	{
		public Default(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
	};
	internal sealed class Logout : Default
	{
		public Logout(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
		public override ReturnValue Execute(ref string[] parameters) => ReturnValue.GetValue(ErrorCodeFlags.IsRequestedLogout, this);
	}
	internal sealed partial class Main : Selection
	{
		public override bool EmptyIsLegal => true;
		public override string GetAskForParameters => null;
		string[] parameters = null;
		public bool Execute()
		{
			parameters = Array.Empty<string>();
			ReturnValue code = Execute(ref parameters);
			if (code.Flags.HasFlag(ErrorCodeFlags.IsSuccess))
			{
				_ = Database.DatabaseController.SaveDatabase();
				_ = InputOutput.Print(code.Text);
			}
			else if (code.Flags.HasFlag(ErrorCodeFlags.IsEmpty) && !code.Case.EmptyIsLegal)
			{
				_ = InputOutput.Print(code.Text);
			}
			else if (code.Flags.HasFlag(ErrorCodeFlags.IsHelpNeeded))
			{
				IList<string> casepath = new List<string>();
				foreach (Base C in code.Case.CasePath())
				{
					casepath.Add(C.CaseName);
				}
				_ = InputOutput.Print(code.Text + string.Join("-", casepath) + "\n\n" + "Help" + "\n" + code.Case.Help + "\n\n" + UserMessages.General.SyntaxHead + "\n" + code.Case.Syntax);
			}
			else if (code.Flags.HasFlag(ErrorCodeFlags.IsRequestedLogout))
			{
				return false;
			}
			else if (code.Flags.HasFlag(ErrorCodeFlags.IsError))
			{
				_ = InputOutput.Print("Error");
				_ = InputOutput.Print(code.Text);
			}
			return true;
		}
	}

}

