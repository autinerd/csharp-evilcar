namespace CsharpEvilcar.UserInterface
{
		
	internal static class ReturnValue
	{
		public static Typ Execute(Typ i) => i;
		public static Typ Execute(out Typ r,Typ i )
		{
			r = i;
			return r;
		}
		internal abstract class Typ
		{
			public Prompt.CaseTyps.Base Case = null;
			private System.Type ClassTyp => typeof(Typ);
			public Typ(Prompt.CaseTyps.Base Case = null) => this.Case = Case;


			private const bool _IsError		= false;
			private const bool _IsPass			= false;
			private const bool _IsHelpNeeded	= false;
			private const bool _IsSuccess		= false;
			private const bool _IsEmpty		= false;
			private const bool _IsWrongArgument = false;
			private const bool _IsDatabaseError = false;
			private const bool _IsNoUserLoggedIn = false;
			private const bool _IsCommandAbort	= false;
			private const bool _IsWrongParameterLength = false;
			private const bool _CommandFunctionUndefined = false;
			private const bool _IsRequestedLogout = false;
			private const string _Text = "Type-return-undefined";


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
				private new const bool _IsError = false;
				private new const string _Text = Prompt.Error.DefaultError;

				public override bool IsError=> _IsError;
				public override string Text => _Text;
				public Error(Prompt.CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class RequestedLogout : Error
				{
					private new const bool _IsRequestedLogout = true;
					private new const string _Text = Prompt.Error.RequestedLogout;

					public override bool IsRequestedLogout => _IsRequestedLogout;
					public override string Text => _Text;
					public RequestedLogout(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class HelpNeeded : Error
				{
					private new const bool _IsHelpNeeded = true;
					private new const string _Text = Prompt.Error.HelpNeeded;

					public override bool IsHelpNeeded => _IsHelpNeeded;
					public override string Text => _Text;
					public HelpNeeded(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongArgument : Error
				{
					private new const bool _IsWrongArgument = true;
					private new const string _Text = Prompt.Error.WrongArgument;

					public override bool IsWrongArgument => _IsWrongArgument;
					public override string Text => _Text;
					public WrongArgument(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class DatabaseError : Error
				{
					private new const bool _IsDatabaseError = true;
					private new const string _Text = Prompt.Error.DatabaseError;

					public override bool IsDatabaseError=> _IsDatabaseError;
					public override string Text => _Text;
					public DatabaseError(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class NoUserLoggedIn : Error
				{
					private new const bool _IsNoUserLoggedIn = true;
					private new const string _Text = Prompt.Error.NoUserLoggedIn;

					public override bool IsNoUserLoggedIn=> _IsNoUserLoggedIn;
					public override string Text => _Text;
					public NoUserLoggedIn(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class CommandAbort : Error
				{
					private new const bool _IsCommandAbort = true;
					private new const string _Text = Prompt.Error.CommandAbort;

					public override bool IsCommandAbort=> _IsCommandAbort;
					public override string Text => _Text;
					public CommandAbort(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongParameterLength : Error
				{
					private new const bool _IsWrongParameterLength = true;
					private new const string _Text = Prompt.Error.WrongParameterLength;

					public override bool IsWrongParameterLength=> _IsWrongParameterLength;
					public override string Text => _Text;
					public WrongParameterLength(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class CommandFunctionUndefined : Error
				{
					private new const bool _CommandFunctionUndefined = true;
					private new const string _Text = Prompt.Error.CommandFunctionUndefined;

					public override bool IsCommandFunctionUndefined => _CommandFunctionUndefined;
					public override string Text => _Text;
					public CommandFunctionUndefined(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
			}
			internal abstract class Pass : Typ
			{
				private new const bool _IsPass = true;
				public new const string _Text = "Pass-return";

				public override bool IsPass => _IsPass;
				public override string Text => _Text;
				public Pass(Prompt.CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class Success : Pass
				{
					private new const bool _IsSuccess = true;
					private new const string _Text = "Success-return";

					public override bool IsSuccess => _IsSuccess;
					public override string Text => _Text;
					public Success(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class Empty : Pass
				{
					private new const bool _IsEmpty = true;
					private new const string _Text = "Empty-return";

					public override bool IsEmpty => _IsEmpty;
					public override string Text => _Text;
					public Empty(Prompt.CaseTyps.Base Case = null) : base(Case) { }
				}

			}
		}
		
		internal static Typ Success(Prompt.CaseTyps.Base Case=null)				=> new Typ.Pass.Success(Case);
		internal static Typ Empty(Prompt.CaseTyps.Base Case=null)					=> new Typ.Pass.Empty(Case);
		internal static Typ WrongArgument(Prompt.CaseTyps.Base Case = null)		=> new Typ.Error.WrongArgument(Case);
		internal static Typ DatabaseError(Prompt.CaseTyps.Base Case = null)		=> new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ NoUserLoggedIn(Prompt.CaseTyps.Base Case = null)		=> new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ CommandAbort(Prompt.CaseTyps.Base Case = null)			=> new Typ.Error.CommandAbort(Case);
		internal static Typ WrongParameterLength(Prompt.CaseTyps.Base Case= null)	=> new Typ.Error.WrongParameterLength(Case);
		internal static Typ HelpNeeded(Prompt.CaseTyps.Base Case = null)			=> new Typ.Error.HelpNeeded(Case);
		internal static Typ CommandFunctionUndefined(Prompt.CaseTyps.Base Case	= null)			=> new Typ.Error.CommandFunctionUndefined(Case);
		internal static Typ RequestedLogout(Prompt.CaseTyps.Base Case = null)		=> new Typ.Error.RequestedLogout(Case);
	}
	
}
