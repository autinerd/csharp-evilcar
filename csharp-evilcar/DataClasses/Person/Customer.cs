using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Customer : Person
	{
		public int ID { set; get; }
		public IEnumerable<Booking> Bookings { set; get; }
	}
}
