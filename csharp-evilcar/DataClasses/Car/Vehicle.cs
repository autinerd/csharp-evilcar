using System.Linq;
using System;

namespace CsharpEvilcar.DataClasses
{
	internal abstract partial class Vehicle : GuidObject
	{
		public abstract Service[] Services { get; }
		public abstract decimal DayPrice { get; }
		public string Numberplate { get; set; }
		public string Model { get; private set; }
		public string Brand { get; private set; }
		public int VehicleID { get; set; }
		public CategoryEnum Category { get; protected set; }
		public decimal TotalDayPrice => ( from svc in Services select svc.Price ).Sum() + DayPrice;

		protected Vehicle(
			string numberplate,
			string model,
			string brand,
			bool hasVehID) : base()
		{
			Numberplate = numberplate;
			Model = model;
			Brand = brand;
			if (!hasVehID)
			{
				VehicleID = ( from b in Database.DatabaseController.DatabaseObject.Branches
							  from f in b.Fleets
							  from v in f.Vehicles
							  select v.VehicleID ).Max() + 1;
			}
		}

		public sealed override string ToString() => $"{Enum.GetName(typeof(CategoryEnum), Category)} Vehicle {VehicleID}: {Brand} {Model} ({Numberplate})";
    }
}
