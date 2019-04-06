using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class Vehicle
	{
		public struct _Service
		{
			readonly public decimal Price ;
			readonly public string Name;
			public _Service(string Name, decimal Price):this()
			{
				this.Name	= Name;
				this.Price	= Price;
			}
		}

		public readonly static _Service _Navigation				= new _Service("Navigation",				 5);
		public readonly static _Service _Massage				= new _Service("Massage",					15);
		public readonly static _Service _ChargingStationFinder	= new _Service("Charging Station Finder",	10);
		public readonly static _Service _Spotify				= new _Service("Spotify",					 8);
		public readonly static _Service _AirConditioner			= new _Service("Air Conditioner",			10);
		public readonly static _Service _SnowChains				= new _Service("Snow Chains",				20);

		public readonly static _Service[] Services = { }; 
		public readonly static decimal DayPrice;
		public readonly string Numberplate;
		public readonly string Type;
		public readonly string Brand;


		public Vehicle(string Numberplate,string Type, string Brand) {
			this.Numberplate = Numberplate;
			this.Type		 = Type;
			this.Brand		 = Brand;
		}
	}
}
