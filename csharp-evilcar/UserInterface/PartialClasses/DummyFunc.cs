﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{

	internal static partial class UserInterface
	{
		public static ErrorCode DummyFunc(IEnumerable<string> parameters)
		{
			Console.WriteLine("##### Dummy Func #####");
			for (int i = 0; i < parameters.Count(); i++)
			{
				Console.WriteLine("[{0,2}]\t{1}",i,parameters.ElementAt(i));
			}
			Console.WriteLine("##### ########## #####");
			return ErrorCode.Success;
		}

	}
}