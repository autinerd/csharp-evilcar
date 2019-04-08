using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class SmallVehicle : Vehicle
	{
		public new static readonly decimal DayPrice = 30;
		public new static readonly _Service[] Services = { _Navigation };
		public SmallVehicle(string Numberplate, string Type, string Brand) : base(Numberplate, Type, Brand)
		{
			Category = CategoryEnum.Small;
		}
	}
}
