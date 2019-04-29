namespace CsharpEvilcar.UserInterface
{
	/*
	internal enum ErrorCode
	{
		Success ,
		WrongArgument,
		DatabaseError,
		NoUserLoggedIn,
		CommandAbort,
		CommandTooShort,
		CommandTooLong,
		WrongParameterLength,
		HelpNeeded,
		Undefined,
		RequestedLogout
	}
	*/


	
	
	internal static class ReturnValue
	{
		public static Output.CaseTyps.Base Case = null;
		internal abstract class Type
		{
			public const bool error = true;
			public volatile string Text = "Error-undefined";
		}
		internal class ErrorReturnValue : Type
		{
			public new const bool error = true;
			public new string Text = "Error-return-undefined";
		}
		internal class PassReturnValue : Type
		{
			public new const bool error = false;
			public new string Text = "Pass-return-undefined";
		}

		internal static Type Success		= new PassReturnValue();
		internal static Type Empty			= new PassReturnValue();
		internal static Type WrongArgument	= new ErrorReturnValue();
		internal static Type DatabaseError	= new ErrorReturnValue();
		internal static Type NoUserLoggedIn	= new ErrorReturnValue();
		internal static Type CommandAbort	= new ErrorReturnValue();
		internal static Type WrongParameterLength = new ErrorReturnValue();
		internal static Type HelpNeeded		= new ErrorReturnValue();
		internal static Type Undefined		= new ErrorReturnValue();
		internal static Type RequestedLogout = new ErrorReturnValue();
	}
	
}
