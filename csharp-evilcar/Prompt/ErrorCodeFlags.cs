using System;

namespace CsharpEvilcar.Prompt
{
	[Flags]
	internal enum ErrorCodeFlags
	{
		None = 0,
		// General Codes
		IsError = 0x001,
		IsPass = 0x002,
		IsHelpNeeded = 0x004,
		IsRequestedLogout = 0x008,
		// Specified Error Codes
		IsWrongArgument = 0x021,
		IsDatabaseError = 0x041,
		IsNoUserLoggedIn = 0x081,
		IsCommandAbort = 0x101,
		IsWrongParameterLength = 0x201,
		IsCommandFunctionUndefined = 0x401,
		// Specific Pass Codes
		IsEmpty = 0x012,
		IsSuccess = 0x802,
	}
}

