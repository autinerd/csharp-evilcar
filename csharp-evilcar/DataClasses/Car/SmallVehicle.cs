namespace CsharpEvilcar.DataClasses
{
	sealed class SmallVehicle : Vehicle
	{
		public new static decimal DayPrice => 30;
		public new static Service[] Services => new Service[] { Navigation };
		public SmallVehicle(string numberplate, string model, string brand, bool hasVehID) : base(numberplate, model, brand, hasVehID)
		{
			Category = CategoryEnum.Small;
		}
	}
}
