using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Fleet : GuidObject
	{
		public IEnumerable<Vehicle> Vehicles { get; set; } = null;
	}
}
