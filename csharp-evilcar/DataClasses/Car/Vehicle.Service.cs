namespace CsharpEvilcar.DataClasses
{
	internal abstract partial class Vehicle
	{
		public static readonly Service Navigation = new Service("Navigation", 5);
		public static readonly Service Massage = new Service("Massage", 15);
		public static readonly Service ChargingStationFinder = new Service("Charging Station Finder", 10);
		public static readonly Service Spotify = new Service("Spotify", 8);
		public static readonly Service AirConditioner = new Service("Air Conditioner", 10);
		public static readonly Service SnowChains = new Service("Snow Chains", 20);
		public struct Service
		{
			public decimal Price { get; private set; }
			public string Name { get; private set; }
			public Service(string name, decimal price)
			{
				Name = name;
				Price = price;
			}
		}
    }
}
