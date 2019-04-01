using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Booking
	{
		public CsharpEvilcar.Person.Customer customer	= null;
		public CsharpEvilcar.Car.Vehicle vehicle		= null;
		public DateTime Startdate	= default(DateTime);
		public DateTime Enddate		= default(DateTime);

		public Booking(CsharpEvilcar.Person.Customer)
	}
}
