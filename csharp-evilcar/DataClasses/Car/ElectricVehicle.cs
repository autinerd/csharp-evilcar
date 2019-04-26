namespace CsharpEvilcar.DataClasses
{
	class ElectricVehicle : Vehicle
	{
		public static new decimal DayPrice => 130;
		public static new Service[] Services => new Service[] { Navigation, ChargingStationFinder, Spotify, AirConditioner, SnowChains };
		public ElectricVehicle(
			string numberplate,
			string type,
			string brand,
			bool hasVehID) : base(
				numberplate,
				type,
				brand,
				hasVehID) => Category = CategoryEnum.Electric;
	}
}
