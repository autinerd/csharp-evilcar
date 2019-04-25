namespace CsharpEvilcar.DataClasses
{
	class LargeVehicle : Vehicle
	{
		public new static decimal DayPrice => 90;
		public new static Service[] Services => new Service[] { Navigation, Massage, Spotify, AirConditioner, SnowChains };
		public LargeVehicle(string numberplate, string type, string brand, bool hasVehID) : base(numberplate, type, brand, hasVehID)
		{
			Category = CategoryEnum.Large;
		}
	}
}
