using System.Collections.Generic;
using System.Linq;

namespace CsharpEvilcar.DataClasses
{
	/// <summary>
	/// Represents a customer.
	/// </summary>
	class Customer : Person
	{
		/// <summary>
		/// The customer ID.
		/// </summary>
		public int CustomerID { set; get; }

		/// <summary>
		/// List of bookings.
		/// </summary>
		public List<Booking> Bookings { set; get; }

		public Customer(bool hasCustID) : base()
		{
			if (!hasCustID)
			{
				CustomerID = ( from c in Database.DatabaseController.Database.Customers select c.CustomerID ).Max() + 1;
			}
		}
	}
}
