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
		public List<DataClasses.Customer> Customers { get; set; }
		/// <summary>
		/// Branch of the current user
		/// </summary>
		public DataClasses.Branch MyBranch => (from b in DatabaseController.Database.Branches where b.Editable select b).Single();
	}
}
