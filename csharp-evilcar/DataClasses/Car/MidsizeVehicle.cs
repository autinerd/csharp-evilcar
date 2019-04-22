using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class MidsizeVehicle : Vehicle
	{
		public new static readonly decimal DayPrice = 60;
		public new static readonly Service[] Services = { Navigation, Spotify, AirConditioner };
		public MidsizeVehicle(string Numberplate, string Type, string Brand) : base(Numberplate, Type, Brand)
		{
			Category = CategoryEnum.Midsize;
		}
	}
}
