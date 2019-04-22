using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class ElectricVehicle : Vehicle
	{
		public new static readonly decimal DayPrice = 130;
		public new static readonly Service[] Services = { Navigation, ChargingStationFinder, Spotify, AirConditioner, SnowChains };
		public ElectricVehicle(string Numberplate, string Type, string Brand) : base(Numberplate, Type, Brand)
		{
			Category = CategoryEnum.Electric;
		}
	}
}
