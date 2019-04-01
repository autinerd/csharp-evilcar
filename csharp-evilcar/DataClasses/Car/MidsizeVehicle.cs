using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.DataClasses
{
	class MidsizeVehicle : Vehicle
	{
		protected readonly static VehicleService Navigation				= new VehicleService(_Navigation,				true);
		protected readonly static VehicleService Massage				= new VehicleService(_Massage,					false);
		protected readonly static VehicleService ChargingStationFinder	= new VehicleService(_ChargingStationFinder,	false);
		protected readonly static VehicleService Spotify				= new VehicleService(_Spotify,					true);
		protected readonly static VehicleService AirConditioner			= new VehicleService(_AirConditioner,			true);
		protected readonly static VehicleService SnowChains				= new VehicleService(_SnowChains,				false);
		public MidsizeVehicle(string Numberplate) : base(Numberplate) { }
	}
}
