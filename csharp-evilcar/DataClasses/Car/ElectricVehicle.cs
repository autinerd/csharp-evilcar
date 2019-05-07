namespace CsharpEvilcar.DataClasses
{
	class ElectricVehicle : Vehicle
	{
		public override decimal DayPrice => 130;
		public override Service[] Services => new Service[] { Navigation, ChargingStationFinder, Spotify, AirConditioner, SnowChains };
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
