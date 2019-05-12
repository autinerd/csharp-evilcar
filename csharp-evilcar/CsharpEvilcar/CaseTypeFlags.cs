using System;

namespace CsharpEvilcar
{
	[Flags]
	internal enum CaseTypeFlags : uint
	{
		// Level 0
		None = 0,
		Add = 0x01,
		Delete = 0x02,
		Edit = 0x04,
		View = 0x08,
		Logout = 0x10,
		Rent = 0x20,
		Return = 0x40,

		// Level 1
		Vehicle = 0x01_0000,
		Customer = 0x02_0000,
		Start = 0x04_0000,
		Stop = 0x08_0000,
		Booking = 0x10_0000,
		Branch = 0x20_0000,
		Fleet = 0x40_0000,
		Password = 0x80_0000
	}
}
