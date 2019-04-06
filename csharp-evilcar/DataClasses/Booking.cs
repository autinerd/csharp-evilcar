using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Booking
	{
		readonly public CsharpEvilcar.DataClasses.Customer Customer	= null;
		readonly public CsharpEvilcar.DataClasses.Vehicle Vehicle	= null;
		readonly public DateTime Startdate	= default(DateTime);
		public DateTime Enddate				= default(DateTime);

		public Booking(Customer Customer,Vehicle Vehicle, DateTime Startdate) {
			this.Customer	= Customer;
			this.Vehicle	= Vehicle;
			this.Startdate	= Startdate;
		}
		public void ReturnVehicle(DateTime Enddate){

		}

	}
}
