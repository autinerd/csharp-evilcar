using System;

namespace CsharpEvilcar
{
    [Flags]
    internal enum CaseTypeFlags
    {
        Main = 0,
        Add = 0x01,
        Book = 0x02,
        Delete = 0x04,
        Edit = 0x08,
        View = 0x10,
        Logout = 0x20,

        Vehicle = 0x0100,
        Customer = 0x0200,
        Start = 0x0400,
        Stop = 0x0800,
        Booking = 0x1000,
        Branch = 0x2000,
        Fleet = 0x4000,
        Password = 0x8000

    }
}