namespace CsharpEvilcar.DataClasses
{
	class LargeVehicle : Vehicle
	{
		public override decimal DayPrice => 90;
		public override Service[] Services => new Service[] { Navigation, Massage, Spotify, AirConditioner, SnowChains };
		public LargeVehicle(
			string numberplate,
			string type,
			string brand,
			bool hasVehID) : base(
				numberplate,
				type,
				brand,
				hasVehID) => Category = CategoryEnum.Large;
	}
}
