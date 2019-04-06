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
		public new static readonly _Service[] Services = { _Navigation, _Spotify, _AirConditioner };
		public MidsizeVehicle(string Numberplate, string Type, string Brand) : base(Numberplate, Type, Brand) { }
	}
}
