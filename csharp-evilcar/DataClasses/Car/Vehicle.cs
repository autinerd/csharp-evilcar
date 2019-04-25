using System.Linq;

namespace CsharpEvilcar.DataClasses
{
	internal abstract class Vehicle : GuidObject
	{
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
		public static Service[] Services { get; private set; }

		public readonly static Service Navigation = new Service("Navigation", 5);
		public readonly static Service Massage = new Service("Massage", 15);
		public readonly static Service ChargingStationFinder = new Service("Charging Station Finder", 10);
		public readonly static Service Spotify = new Service("Spotify", 8);
		public readonly static Service AirConditioner = new Service("Air Conditioner", 10);
		public readonly static Service SnowChains = new Service("Snow Chains", 20);

		public static decimal DayPrice { get; private set; }
		public string Numberplate { get; private set; }
		public string Model { get; private set; }
		public string Brand { get; private set; }
		public int VehicleID { get; private set; }
		public CategoryEnum Category { get; protected set; }

		internal enum CategoryEnum
		{
			Small,
			Midsize,
			Large,
			Electric
		}

		protected Vehicle(string numberplate, string model, string brand, bool hasVehID) : base()
		{
			Numberplate = numberplate;
			Model = model;
			Brand = brand;
			if (hasVehID)
			{
				VehicleID = ( from b in Database.DatabaseController.Database.Branches
							  from f in b.Fleets
							  from v in f.Vehicles
							  select v.VehicleID ).Max() + 1;
			}
		}
	}
}