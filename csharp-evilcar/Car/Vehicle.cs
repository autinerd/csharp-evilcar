using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.Car
{
	class Vehicle
	{
		public struct _Service
		{
			public decimal Price ;
			readonly public string Name;
			public _Service(string Name, decimal Price):this()
			{
				this.Name	= Name;
				this.Price	= Price;
			}
		}
		public struct VehicleService
		{
			readonly bool Possible;
			readonly _Service Service;
			private bool _Available;
			public bool Available
			{
				set
				{
					if (!Possible && value)
					{
						throw new Exception();
					}
					else
					{
						_Available = value;
					}
				}
				get { return _Available; }
			}
			public VehicleService(_Service service,bool possible)
			{
				Possible = possible;
				Service = service;
				_Available = possible;
			}
		}
		protected readonly static _Service _Navigation				= new _Service("Navigation",				 5);
		protected readonly static _Service _Massage					= new _Service("Massage",					15);
		protected readonly static _Service _ChargingStationFinder	= new _Service("Charging Station Finder",	10);
		protected readonly static _Service _Spotify					= new _Service("Spotify",					 8);
		protected readonly static _Service _AirConditioner			= new _Service("Air Conditioner",			10);
		protected readonly static _Service _SnowChains				= new _Service("Snow Chains",				20);
		//protected readonly static _Service[] _Services = { _Navigation, _Massage, _ChargingStationFinder, _Spotify, _AirConditioner, _SnowChains };
		public readonly string Numberplate;
		public Vehicle(string Numberplate)
		{
			this.Numberplate = Numberplate;
		}
	}
}
