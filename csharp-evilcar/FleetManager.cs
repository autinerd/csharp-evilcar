using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar
{
	class FleetManager
	{
		private string _name;
		private Branch _branch;

		public string name
		{
			get { return _name; }
			set { _name = value; }
		}
		public Branch branch
		{
			get { return _branch; }
			set { _branch = branch; }
		}
	}
}
