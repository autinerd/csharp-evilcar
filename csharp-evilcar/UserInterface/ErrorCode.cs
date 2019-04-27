namespace CsharpEvilcar.UserInterface
{
	internal enum ErrorCode
	{
		Success,
		WrongArgument,
		DatabaseError,
		NoUserLoggedIn,
		CommandAbort,
		CommandTooShort,
		CommandTooLong,
		HelpNeeded
	}
}
