namespace CsharpEvilcar.DataClasses
{
	class MidsizeVehicle : Vehicle
	{
		public new static decimal DayPrice => 60;
		public new static Service[] Services => new Service[] { Navigation, Spotify, AirConditioner };
		public MidsizeVehicle(string numberplate, string type, string brand, bool hasVehID) : base(numberplate, type, brand, hasVehID)
		{
			Category = CategoryEnum.Midsize;
		}
	}
}
