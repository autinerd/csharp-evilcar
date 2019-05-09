using System;

namespace CsharpEvilcar.Prompt
{
	internal static class ConsoleStuff
	{
		/// <summary>
		/// Enables QuickEdit mode for the console.
		/// </summary>
		/// <returns>successful</returns>
		internal static bool EnableQuickEdit()
		{
			IntPtr consoleHandle = NativeMethods.GetStdHandle(NativeMethods.STD_INPUT_HANDLE);

			if (!NativeMethods.GetConsoleMode(consoleHandle, out uint consoleMode))
			{
				return false;
			}
			consoleMode |= NativeMethods.ENABLE_QUICK_EDIT;
			return NativeMethods.SetConsoleMode(consoleHandle, consoleMode);
		}
	}
}
