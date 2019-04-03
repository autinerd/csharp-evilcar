using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.Database
{
	/// <summary>
	/// The class which represents the database structure
	/// </summary>
	internal class Database
	{
		/// <summary>
		/// List of branches
		/// </summary>
		public IEnumerable<DataClasses.Branch> Branches { get; set; }
		/// <summary>
		/// List of customers
		/// </summary>
		public IEnumerable<DataClasses.Customer> Customers { get; set; }
	}
}
