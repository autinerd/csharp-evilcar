namespace CsharpEvilcar.DataClasses
{
	class LargeVehicle : Vehicle
	{
		public static new decimal DayPrice => 90;
		public static new Service[] Services => new Service[] { Navigation, Massage, Spotify, AirConditioner, SnowChains };
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
