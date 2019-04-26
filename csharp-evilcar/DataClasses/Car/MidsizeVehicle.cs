namespace CsharpEvilcar.DataClasses
{
	class MidsizeVehicle : Vehicle
	{
		public static new decimal DayPrice => 60;
		public static new Service[] Services => new Service[] { Navigation, Spotify, AirConditioner };
		public MidsizeVehicle(
			string numberplate,
			string type,
			string brand,
			bool hasVehID) : base(
				numberplate,
				type,
				brand,
				hasVehID) => Category = CategoryEnum.Midsize;
	}
}
