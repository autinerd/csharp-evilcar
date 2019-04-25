using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class ElectricVehicle : Vehicle
	{
		public new static decimal DayPrice => 130;
		public new static Service[] Services => new Service[] { Navigation, ChargingStationFinder, Spotify, AirConditioner, SnowChains };
		public ElectricVehicle(string numberplate, string type, string brand, bool hasVehID) : base(numberplate, type, brand, hasVehID)
		{
			Category = CategoryEnum.Electric;
		}
	}
}
