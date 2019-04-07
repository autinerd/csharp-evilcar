using System.Collections.Generic;

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
		public IEnumerable<Booking> Bookings { set; get; }
	}
}
