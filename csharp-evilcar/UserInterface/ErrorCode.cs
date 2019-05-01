namespace CsharpEvilcar.UserInterface
{

	internal static class ReturnValue
	{
		public static Typ Execute(Typ i) => i;
		public static Typ Execute(out Typ r, Typ i)
		{
			r = i;
			return r;
		}
		internal abstract class Typ
		{
			public Prompt.CaseTyps.Base Case = null;
			protected System.Type ClassTyp => typeof(Typ);
			public Typ(Prompt.CaseTyps.Base Case = null) => this.Case = Case;


			protected const bool _IsError = false;
			protected const bool _IsPass = false;
			protected const bool _IsHelpNeeded = false;
			protected const bool _IsSuccess = false;
			protected const bool _IsEmpty = false;
			protected const bool _IsWrongArgument = false;
			protected const bool _IsDatabaseError = false;
			protected const bool _IsNoUserLoggedIn = false;
			protected const bool _IsCommandAbort = false;
			protected const bool _IsWrongParameterLength = false;
			protected const bool _CommandFunctionUndefined = false;
			protected const bool _IsRequestedLogout = false;
			protected const string _Text = "Type-return-undefined";


			public virtual string Text => _Text;
			public virtual bool IsError => _IsError;
			public virtual bool IsPass => _IsPass;
			public virtual bool IsHelpNeeded => _IsHelpNeeded;
			public virtual bool IsSuccess => _IsSuccess;
			public virtual bool IsEmpty => _IsEmpty;
			public virtual bool IsWrongArgument => _IsWrongArgument;
			public virtual bool IsDatabaseError => _IsDatabaseError;
			public virtual bool IsNoUserLoggedIn => _IsNoUserLoggedIn;
			public virtual bool IsCommandAbort => _IsCommandAbort;
			public virtual bool IsWrongParameterLength => _IsWrongParameterLength;
			public virtual bool IsCommandFunctionUndefined => _CommandFunctionUndefined;
			public virtual bool IsRequestedLogout => _IsRequestedLogout;

			internal abstract class Error : Typ
			{
				protected new const bool _IsError = false;
				protected new const string _Text = Prompt.Error.DefaultError;

				public override bool IsError => _IsError;
				public override string Text => _Text;
				public Error(Prompt.CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class RequestedLogout : Error
				{
					protected const bool _IsRequestedLogout = true;
					protected new const string _Text = Prompt.Error.RequestedLogout;

					public override bool IsRequestedLogout => _IsRequestedLogout;
					public override string Text => _Text;
					public RequestedLogout(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class HelpNeeded : Error
				{
					protected new const bool _IsHelpNeeded = true;
					protected new const string _Text = Prompt.Error.HelpNeeded;

					public override bool IsHelpNeeded => _IsHelpNeeded;
					public override string Text => _Text;
					public HelpNeeded(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongArgument : Error
				{
					protected new const bool _IsWrongArgument = true;
					protected new const string _Text = Prompt.Error.WrongArgument;

					public override bool IsWrongArgument => _IsWrongArgument;
					public override string Text => _Text;
					public WrongArgument(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class DatabaseError : Error
				{
					protected new const bool _IsDatabaseError = true;
					protected new const string _Text = Prompt.Error.DatabaseError;

					public override bool IsDatabaseError => _IsDatabaseError;
					public override string Text => _Text;
					public DatabaseError(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class NoUserLoggedIn : Error
				{
					protected new const bool _IsNoUserLoggedIn = true;
					protected new const string _Text = Prompt.Error.NoUserLoggedIn;

					public override bool IsNoUserLoggedIn => _IsNoUserLoggedIn;
					public override string Text => _Text;
					public NoUserLoggedIn(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class CommandAbort : Error
				{
					protected new const bool _IsCommandAbort = true;
					protected new const string _Text = Prompt.Error.CommandAbort;

					public override bool IsCommandAbort => _IsCommandAbort;
					public override string Text => _Text;
					public CommandAbort(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongParameterLength : Error
				{
					protected new const bool _IsWrongParameterLength = true;
					protected new const string _Text = Prompt.Error.WrongParameterLength;

					public override bool IsWrongParameterLength => _IsWrongParameterLength;
					public override string Text => _Text;
					public WrongParameterLength(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class CommandFunctionUndefined : Error
				{
					protected new const bool _CommandFunctionUndefined = true;
					protected new const string _Text = Prompt.Error.CommandFunctionUndefined;

					public override bool IsCommandFunctionUndefined => _CommandFunctionUndefined;
					public override string Text => _Text;
					public CommandFunctionUndefined(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
			}
			internal abstract class Pass : Typ
			{
				protected new const bool _IsPass = true;
				public new const string _Text = "Pass-return";

				public override bool IsPass => _IsPass;
				public override string Text => _Text;
				public Pass(Prompt.CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class Success : Pass
				{
					protected new const bool _IsSuccess = true;
					protected new const string _Text = Prompt.Pass.Success;

					public override bool IsSuccess => _IsSuccess;
					public override string Text => _Text;
					public Success(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class Empty : Pass
				{
					protected new const bool _IsEmpty = true;
					protected new const string _Text = "Empty-return";

					public override bool IsEmpty => _IsEmpty;
					public override string Text => _Text;
					public Empty(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}

			}
		}

		internal static Typ Success(Prompt.CaseTyps.Base Case = null) => new Typ.Pass.Success(Case);
		internal static Typ Empty(Prompt.CaseTyps.Base Case = null) => new Typ.Pass.Empty(Case);
		internal static Typ WrongArgument(Prompt.CaseTyps.Base Case = null) => new Typ.Error.WrongArgument(Case);
		internal static Typ DatabaseError(Prompt.CaseTyps.Base Case = null) => new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ NoUserLoggedIn(Prompt.CaseTyps.Base Case = null) => new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ CommandAbort(Prompt.CaseTyps.Base Case = null) => new Typ.Error.CommandAbort(Case);
		internal static Typ WrongParameterLength(Prompt.CaseTyps.Base Case = null) => new Typ.Error.WrongParameterLength(Case);
		internal static Typ HelpNeeded(Prompt.CaseTyps.Base Case = null) => new Typ.Error.HelpNeeded(Case);
		internal static Typ CommandFunctionUndefined(Prompt.CaseTyps.Base Case = null) => new Typ.Error.CommandFunctionUndefined(Case);
		internal static Typ RequestedLogout(Prompt.CaseTyps.Base Case = null) => new Typ.Error.RequestedLogout(Case);
	}

}
