namespace CsharpEvilcar.DataClasses
{
	class MidsizeVehicle : Vehicle
	{
		public override decimal DayPrice => 60;
		public override Service[] Services => new Service[] { Navigation, Spotify, AirConditioner };
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
