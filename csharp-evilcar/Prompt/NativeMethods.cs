using System;
using System.Runtime.InteropServices;

namespace CsharpEvilcar.Prompt
{
	internal static class NativeMethods
	{
		internal const int ENABLE_QUICK_EDIT = 0x40;
		internal const int STD_INPUT_HANDLE = -10;

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr GetStdHandle(int nStdHandle);

		[DllImport("kernel32.dll")]
		internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

		[DllImport("kernel32.dll")]
		internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
	}
}
