﻿using System.Collections.Generic;
using static CsharpEvilcar.Prompt.ErrorCodeFlags;

namespace CsharpEvilcar
{
	internal static partial class UserMessages
	{
		internal static Dictionary<Prompt.ErrorCodeFlags, string> Messages => new Dictionary<Prompt.ErrorCodeFlags, string>
		{
			{ IsSuccess, "done."},
			{ IsEmpty, "done." },
			{ IsPass, "" },
			{ IsError, "An unspecified error happened" },
			{ IsHelpNeeded, "You requested help in Case: " },
			{ IsWrongArgument, "Some of the entered arguments were wrong." },
			{ IsDatabaseError, "A database error happend." },
			{ IsNoUserLoggedIn, "You are not logged in." },
			{ IsCommandAbort, "The command was not executed." },
			{ IsWrongParameterLength, "The amount of entered parameters doesn't match the amount needed." },
			{ IsCommandFunctionUndefined, "The command could not be understood." },
			{ None, "" }
		};
	}
}
