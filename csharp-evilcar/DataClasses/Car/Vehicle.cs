namespace CsharpEvilcar.DataClasses
{
	internal class Vehicle : GuidObject
	{
		public struct Service
		{
			readonly public decimal Price;
			readonly public string Name;
			public Service(string Name, decimal Price) : this()
			{
				this.Name = Name;
				this.Price = Price;
			}
		}

		public readonly static Service Navigation = new Service("Navigation", 5);
		public readonly static Service Massage = new Service("Massage", 15);
		public readonly static Service ChargingStationFinder = new Service("Charging Station Finder", 10);
		public readonly static Service Spotify = new Service("Spotify", 8);
		public readonly static Service AirConditioner = new Service("Air Conditioner", 10);
		public readonly static Service SnowChains = new Service("Snow Chains", 20);

		public readonly static Service[] Services = { };
		public readonly static decimal DayPrice;
		public readonly string Numberplate;
		public readonly string Type;
		public readonly string Brand;
		public CategoryEnum Category { get; protected set; }

		internal enum CategoryEnum
		{
			Small,
			Midsize,
			Large,
			Electric
		}


		public Vehicle(string Numberplate, string Type, string Brand)
		{
			this.Numberplate = Numberplate;
			this.Type = Type;
			this.Brand = Brand;
		}
	}
}
