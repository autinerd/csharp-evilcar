using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class LargeVehicle : Vehicle
	{
		public new static readonly decimal DayPrice = 90;
		public new static readonly _Service[] Services = { _Navigation, _Massage, _Spotify, _AirConditioner, _SnowChains };
		public LargeVehicle(string Numberplate, string Type, string Brand) : base(Numberplate,Type,Brand)
		{
			Category = CategoryEnum.Large;
		}
	}
}
