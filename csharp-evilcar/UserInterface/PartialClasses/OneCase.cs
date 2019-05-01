using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class Prompt
	{
		internal class CaseTyps
		{
			internal abstract class Base
			{
				public Base(Base ParentsCase=null) => this.ParentsCase = ParentsCase;
				public virtual ReturnValue.Typ Init(Base ParentsCase = null) {
					this.ParentsCase = ParentsCase;
					return ReturnValue.Success(this);
				}
				public Base ParentsCase=null;
				public string CaseName = null;
				public string AskForParameters = null;
				public string Help = null;
				public virtual string Syntax { get; set; }
				public virtual bool EmptyIsLegal => false;

				public IList<Base> CasePath()
				{

					IList<Base> Path = new List<Base>();
					Base CurrentCase = this;
					do{
						Path.Add(CurrentCase);
						CurrentCase = CurrentCase.ParentsCase;
					}
					while ( CurrentCase != null );
					return Path.Reverse().ToList();
				}

				public string GetCaseName => CaseName ?? "CaseName-undefined";
				public virtual string GetAskForParameters => AskForParameters ?? string.Concat(CaseName, "-AskFor-undefinde");
				public string GetHelp => Help ?? string.Concat(CaseName, "-help-undefinde");


				public virtual string GetSyntax => null;
				public virtual ReturnValue.Typ CheckParameterLenght(string[] inputArray) => ReturnValue.Success(this);
				public virtual ReturnValue.Typ Execute(ref string[] parameters)
				{
					if (parameters.Length == 0)
					{
						Print(GetAskForParameters);
						GetInput(out parameters);
					}
					return CheckParameterLenght(parameters);
					
				}

			}
			internal class Selection : Base
			{
				public Selection(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
				public override ReturnValue.Typ Init(Base ParentsCase = null)
				{
					this.ParentsCase = ParentsCase;
					foreach (Base Subcase in SubCases)
					{
						Subcase.Init(this);
					}
					return ReturnValue.Success(this);
				}
				public IEnumerable<Base> SubCases = null;
				public override string Syntax => string.Join("\n", from c in SubCases select c.Syntax) + "\n";
				public override ReturnValue.Typ CheckParameterLenght(string[] inputArray) => inputArray.Count() > 0 ? ReturnValue.Success(this) : ReturnValue.Empty(this);
				public override ReturnValue.Typ Execute(ref string[] parameters)
				{
					if (ReturnValue.Execute(out ReturnValue.Typ code, base.Execute(ref parameters)).IsError|code.IsEmpty)
					{
						return code;
					}
					string selection = parameters[0].ToLower(CultureInfo.CurrentCulture);
					parameters = parameters.Skip(1).ToArray();

					Base selectedCase = ( from s in SubCases
											where s.CaseName == selection
											select s ).SingleOrDefault();

					if (General.HelpSymbol.Contains(selection))
					{
						return ReturnValue.HelpNeeded(this);
					}
					else if (selectedCase == default)
					{
						return ReturnValue.WrongArgument(this);
					}
					else {
						return selectedCase.Execute(ref parameters);
					}
				}
				

			}
			internal class Command : Base
			{

				public Command(Base ParentsCase = null) => this.ParentsCase = ParentsCase;
				public int[] ParameterLength = null;
				public override ReturnValue.Typ CheckParameterLenght(string[] inputArray) => ParameterLength.Contains(inputArray.Count()) ? ReturnValue.Success(this) : ReturnValue.WrongParameterLength(this);
				public Func<IEnumerable<string>, ReturnValue.Typ> SubFunction =null;

				public override ReturnValue.Typ Execute(ref string[] parameters)
				{
					if (AskForParameters != null)
					{
						ReturnValue.Execute(out ReturnValue.Typ code, base.Execute(ref parameters));
						if (code.IsError)
						{
							return code;
						}

					}

					if (parameters.Any(s => General.HelpSymbol.Contains(s)))
					{
						return ReturnValue.HelpNeeded(this);
					}
					else if (CheckParameterLenght(parameters).IsPass)
					{
						return SubFunction == null ? ReturnValue.CommandFunctionUndefined(this) : SubFunction(parameters);
					}
					else
					{
						return ReturnValue.CommandAbort(this);
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
				public override ReturnValue.Typ Execute(ref string[] parameters) => ReturnValue.RequestedLogout(this);
			}
			internal sealed class Main : Selection
			{
				public override bool EmptyIsLegal => true;
				public override string GetAskForParameters => null;
				string[] parameters =null;
				public bool Execute()
				{
					parameters = Array.Empty<string>();
					ReturnValue.Typ code = Execute(ref parameters);
					if (code.IsSuccess)
					{
						Database.DatabaseController.SaveDatabase();
						Print(code.Text);
					}
					else if (code.IsEmpty&!code.Case.EmptyIsLegal)
					{
						Print(code.Text);
					}
					else if (code.IsHelpNeeded)
					{
						IList<string> casepath = new List<string>();
						foreach (Base C in code.Case.CasePath())
						{
							casepath.Add(C.CaseName);
						}
						Print(code.Text + string.Join("-", casepath) + "\n\n"+"Help"+ "\n"+code.Case.Help + "\n\n" + General.SyntaxHead + "\n" + code.Case.Syntax);
					}
					else if (code.IsRequestedLogout)
					{
						return false;
					}
					else if (code.IsError)
					{
						Print("Error");
						Print(code.Text);
					}
					return true;
				}
			}
		}
	}
}


