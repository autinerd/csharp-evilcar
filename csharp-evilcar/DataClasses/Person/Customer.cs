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

		public override string ToString() => ToString(false);

		public string ToString(bool fullDetails) => fullDetails
				? $@"Customer {CustomerID}: Name: {Name}, Residence: {Residence}
	{Bookings.Count} Bookings:
		{string.Join("\n\t\t", from b in Bookings select b.ToString())}"
				: $@"Customer {CustomerID}: Name: {Name}, Residence: {Residence}, {Bookings.Count} Bookings";
	}
}
