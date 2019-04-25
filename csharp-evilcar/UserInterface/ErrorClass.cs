using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal class AbortCommandExecution : Exception
		{
			public AbortCommandExecution() { }
			public AbortCommandExecution(string message) : base(message) { }
			public AbortCommandExecution(string message, Exception inner): base(message, inner){}
		}
	}
}
