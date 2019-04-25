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
		public IEnumerable<DataClasses.Branch> Branches { get; set; }
		/// <summary>
		/// List of customers
		/// </summary>
		public IEnumerable<DataClasses.Customer> Customers { get; set; }

		public DataClasses.Branch MyBranch => (from b in DatabaseController.Database.Branches where b.Editable select b).Single();
	}
}
