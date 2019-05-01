#pragma warning disable CA1822

namespace CsharpEvilcar.Prompt
{
	internal static class ReturnValue
	{
		internal static Typ Execute(Typ i) => i;
		internal static Typ Execute(out Typ r, Typ i)
		{
			r = i;
			return r;
		}
		internal abstract class Typ
		{
			internal CaseTyps.Base Case = null;
			internal Typ(CaseTyps.Base Case = null) => this.Case = Case;
			internal virtual bool IsError => false;
			internal virtual bool IsPass => false;
			internal virtual bool IsHelpNeeded => false;
			internal virtual bool IsSuccess => false;
			internal virtual bool IsEmpty => false;
			internal virtual bool IsWrongArgument => false;
			internal virtual bool IsDatabaseError => false;
			internal virtual bool IsNoUserLoggedIn => false;
			internal virtual bool IsCommandAbort => false;
			internal virtual bool IsWrongParameterLength => false;
			internal virtual bool IsCommandFunctionUndefined => false;
			internal virtual bool IsRequestedLogout => false;
			internal virtual string Text => "Type-return-undefined";

			internal abstract class Error : Typ
			{
				internal override bool IsError => true;
				internal override string Text => UserMessages.Error.DefaultError;
				internal Error(CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class RequestedLogout : Error
				{
					internal override bool IsRequestedLogout => true;
					internal override string Text => UserMessages.Error.RequestedLogout;
					internal RequestedLogout(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class HelpNeeded : Error
				{
					internal override bool IsHelpNeeded => true;
					internal override string Text => UserMessages.Error.HelpNeeded;
					internal HelpNeeded(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongArgument : Error
				{
					internal override bool IsWrongArgument => true;
					internal override string Text => UserMessages.Error.WrongArgument;
					internal WrongArgument(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class DatabaseError : Error
				{
					internal override bool IsDatabaseError => true;
					internal override string Text => UserMessages.Error.DatabaseError;
					internal DatabaseError(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class NoUserLoggedIn : Error
				{
					internal override bool IsNoUserLoggedIn => true;
					internal override string Text => UserMessages.Error.NoUserLoggedIn;
					internal NoUserLoggedIn(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal class CommandAbort : Error
				{
					internal override bool IsCommandAbort => true;
					internal override string Text => UserMessages.Error.CommandAbort;
					internal CommandAbort(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class WrongParameterLength : Error
				{
					internal override bool IsWrongParameterLength => true;
					internal override string Text => UserMessages.Error.WrongParameterLength;
					internal WrongParameterLength(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class CommandFunctionUndefined : Error
				{
					internal override bool IsCommandFunctionUndefined => true;
					internal override string Text => UserMessages.Error.CommandFunctionUndefined;
					internal CommandFunctionUndefined(CaseTyps.Base Case = null) : base(Case) { }
				}
			}
			internal abstract class Pass : Typ
			{
				internal override bool IsPass => true;
				internal override string Text => "Pass-return";
				internal Pass(CaseTyps.Base Case = null) : base(Case) { }

				internal sealed class Success : Pass
				{
					internal override bool IsSuccess => true;
					internal override string Text => UserMessages.Pass.Success;
					internal Success(CaseTyps.Base Case = null) : base(Case) { }
				}
				internal sealed class Empty : Pass
				{
					internal override bool IsEmpty => true;
					internal override string Text => "Empty-return";
					internal Empty(CaseTyps.Base Case = null) : base(Case) { }
				}

			}
		}

		internal static Typ Success(CaseTyps.Base Case = null) => new Typ.Pass.Success(Case);
		internal static Typ Empty(CaseTyps.Base Case = null) => new Typ.Pass.Empty(Case);
		internal static Typ WrongArgument(CaseTyps.Base Case = null) => new Typ.Error.WrongArgument(Case);
		internal static Typ DatabaseError(CaseTyps.Base Case = null) => new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ NoUserLoggedIn(CaseTyps.Base Case = null) => new Typ.Error.NoUserLoggedIn(Case);
		internal static Typ CommandAbort(CaseTyps.Base Case = null) => new Typ.Error.CommandAbort(Case);
		internal static Typ WrongParameterLength(CaseTyps.Base Case = null) => new Typ.Error.WrongParameterLength(Case);
		internal static Typ HelpNeeded(CaseTyps.Base Case = null) => new Typ.Error.HelpNeeded(Case);
		internal static Typ CommandFunctionUndefined(CaseTyps.Base Case = null) => new Typ.Error.CommandFunctionUndefined(Case);
		internal static Typ RequestedLogout(CaseTyps.Base Case = null) => new Typ.Error.RequestedLogout(Case);
	}
}

