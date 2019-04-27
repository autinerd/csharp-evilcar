using System.Collections.Generic;
using System.Linq;

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
		internal IEnumerable<DataClasses.Branch> Branches { get; set; }
		/// <summary>
		/// List of customers
		/// </summary>
		internal List<DataClasses.Customer> Customers { get; set; }
		/// <summary>
		/// Branch of the current user
		/// </summary>
		internal DataClasses.Branch MyBranch => ( from b in Branches where b.Editable select b ).Single();
	}
}
