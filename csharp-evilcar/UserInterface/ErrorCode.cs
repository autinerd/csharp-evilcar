namespace CsharpEvilcar.UserInterface
{
		
	internal static class ReturnValue
	{
		internal abstract class Type
		{
			public Prompt.CaseTyps.Base Case = null;
			public Type(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
			public virtual string Text => "Error-undefined";

			public virtual bool IsError			=> true;
			public virtual bool IsPass			=> false;
			public virtual bool IsHelpNeeded	=> false;
			public virtual bool IsSuccess		=> false;
			public virtual bool IsEmpty			=> false;
			public virtual bool IsWrongArgument => false;
			public virtual bool IsDatabaseError => false;
			public virtual bool IsNoUserLoggedIn => false;
			public virtual bool IsCommandAbort	=> false;
			public virtual bool IsWrongParameterLength => false;
			public virtual bool IsUndefined		=> false;
			public virtual bool IsRequestedLogout => false;

			
			internal class Error : Type
			{
				
				public override string Text => "Error-return-undefined";
				public Error(Prompt.CaseTyps.Base Case = null) => this.Case = Case;

				internal class RequestedLogout : Error
				{
					public override bool IsRequestedLogout => true;
					public RequestedLogout(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class HelpNeeded : Error
				{
					public override bool IsHelpNeeded => true;
					public HelpNeeded(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class WrongArgument : Error
				{
					public override bool IsWrongArgument => true;
					public WrongArgument(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class DatabaseError : Error
				{
					public override bool IsDatabaseError => true;
					public DatabaseError(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class NoUserLoggedIn : Error
				{
					public override bool IsNoUserLoggedIn => true;
					public NoUserLoggedIn(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class CommandAbort : Error
				{
					public override bool IsCommandAbort => true;
					public CommandAbort(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class WrongParameterLength : Error
				{
					public override bool IsWrongParameterLength => true;
					public WrongParameterLength(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class Undefined : Error
				{
					public override bool IsUndefined => true;
					public Undefined(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
			}
			internal class Pass : Type
			{
				public override bool IsPass => true;
				public override bool IsError => false;
				public override string Text => "Pass-return-undefined";
				public Pass(Prompt.CaseTyps.Base Case = null) => this.Case = Case;

				internal class Success : Pass
				{
					public override bool IsSuccess => true;
					public Success(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}
				internal class Empty : Pass
				{
					public override bool IsEmpty => true;
					public Empty(Prompt.CaseTyps.Base Case = null) => this.Case = Case;
				}

			}
		}
		
		internal static Type Success(Prompt.CaseTyps.Base Case=null)				=> new Type.Pass.Success(Case);
		internal static Type Empty(Prompt.CaseTyps.Base Case=null)					=> new Type.Pass.Empty(Case);
		internal static Type WrongArgument(Prompt.CaseTyps.Base Case = null)		=> new Type.Error.WrongArgument(Case);
		internal static Type DatabaseError(Prompt.CaseTyps.Base Case = null)		=> new Type.Error.NoUserLoggedIn(Case);
		internal static Type NoUserLoggedIn(Prompt.CaseTyps.Base Case = null)		=> new Type.Error.NoUserLoggedIn(Case);
		internal static Type CommandAbort(Prompt.CaseTyps.Base Case = null)			=> new Type.Error.CommandAbort(Case);
		internal static Type WrongParameterLength(Prompt.CaseTyps.Base Case= null)	=> new Type.Error.WrongParameterLength(Case);
		internal static Type HelpNeeded(Prompt.CaseTyps.Base Case = null)			=> new Type.Error.HelpNeeded(Case);
		internal static Type Undefined(Prompt.CaseTyps.Base Case = null)			=> new Type.Error.Undefined(Case);
		internal static Type RequestedLogout(Prompt.CaseTyps.Base Case = null)		=> new Type.Error.RequestedLogout(Case);
	}
	
}
