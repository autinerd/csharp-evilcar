using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.Database
{
	internal class Database
	{
		public IEnumerable<DataClasses.Branch> Branches { get; set; }
		public IEnumerable<DataClasses.Customer> Customers { get; set; }
	}
}
