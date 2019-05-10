using CsharpEvilcar.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using CsharpEvilcar.Prompt;
using static CsharpEvilcar.Database.DatabaseController;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		internal static ReturnValue LogoutFromProgram(IEnumerable<string> parammeters)
        {
            return ReturnValue.GetValue(ErrorCodeFlags.IsRequestedLogout);
        }
	}
}