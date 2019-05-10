using System.Collections.Generic;
using CsharpEvilcar.Prompt;

namespace CsharpEvilcar
{
	internal static partial class InternalLogic
	{
		internal static ReturnValue LogoutFromProgram(IEnumerable<string> parameters) => ReturnValue.GetValue(ErrorCodeFlags.IsRequestedLogout);
	}
}
