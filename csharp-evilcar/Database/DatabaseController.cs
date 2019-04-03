using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsharpEvilcar.Database
{
	internal static class DatabaseController
	{
		internal static Database Database { get; private set; } = null;
		internal static Guid CurrentUser
		{
			get
			{
				return currentUser;
			}
			private set
			{
				currentUser = value;
			}
		}
		private static Guid currentUser = Guid.Empty;

		private static readonly Scrypt.ScryptEncoder encoder = new Scrypt.ScryptEncoder();

		internal static int LoadDatabase()
		{
			try
			{
				return MapToDatabase(ReadDatabaseFile());
			}
			catch (Exception)
			{
				return 1;
			}
		}

		internal static int SaveDatabase()
		{
			try
			{
				return SaveDatabaseFile();
			}
			catch (Exception)
			{
				return 1;
			}
		}

		private static int SaveDatabaseFile()
		{
			JObject jObject;
			int returnval = MapToJSON(out jObject);
			if (returnval != 0)
			{
				return returnval;
			}
			try
			{
				using (StreamWriter sw = new StreamWriter("database.json"))
				using (JsonTextWriter writer = new JsonTextWriter(sw))
				{
					jObject.WriteTo(writer);
				}
				return 0;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private static JObject ReadDatabaseFile()
		{
			try
			{
				using (StreamReader reader = new StreamReader("database.json"))
				{
					return JObject.Parse(reader.ReadToEnd());
				}
			}
			catch (Exception)
			{
				throw;
			}

		}

		private static int MapToDatabase(JObject jObject)
		{
			if (CurrentUser == Guid.Empty)
			{
				return 154;
			}
			else
			{
				Database = new Database
				{
					Customers = jObject["Customers"].Select((customer) => new DataClasses.Customer
					{
						GUID = Guid.Parse((string)customer["GUID"]),
						ID = (int)customer["CustomerID"],
						Name = (string)customer["Name"],
						Residence = (string)customer["Residence"],
						Bookings = customer["Bookings"].Select((booking) => new DataClasses.Booking
						{
							GUID = Guid.Parse((string)booking["GUID"]),
							BookingID = (int)booking["BookingID"],
							Startdate = DateTime.ParseExact((string)booking["Startdate"], "yyyyMMdd", null),
							Enddate = DateTime.ParseExact((string)booking["Enddate"], "yyyyMMdd", null),
							VehicleGuid = Guid.Parse((string)booking["Vehicle"])
						})
					}),
					Branches = jObject["Branches"].Select((branch) => new DataClasses.Branch
					{
						GUID = Guid.Parse((string)branch["GUID"]),
						Fleets = branch["Fleets"].Select((fleet) => new DataClasses.Fleet
						{
							GUID = Guid.Parse((string)fleet["GUID"]),
							Vehicles = fleet["Vehicles"].Select<JToken, DataClasses.Vehicle>((vehicle) =>
							{
								switch ((DataClasses.Vehicle.CategoryEnum)(int)vehicle["Category"])
								{
									case DataClasses.Vehicle.CategoryEnum.Small:
										return new DataClasses.SmallVehicle((string)vehicle["Numberplate"])
										{
											GUID = Guid.Parse((string)vehicle["GUID"])
										};
									case DataClasses.Vehicle.CategoryEnum.Midsize:
										return new DataClasses.MidsizeVehicle((string)vehicle["Numberplate"])
										{
											GUID = Guid.Parse((string)vehicle["GUID"])
										};
									case DataClasses.Vehicle.CategoryEnum.Large:
										return new DataClasses.LargeVehicle((string)vehicle["Numberplate"])
										{
											GUID = Guid.Parse((string)vehicle["GUID"])
										};
									case DataClasses.Vehicle.CategoryEnum.Electric:
										return new DataClasses.ElectricVehicle((string)vehicle["Numberplate"])
										{
											GUID = Guid.Parse((string)vehicle["GUID"])
										};
									default:
										return null;
								}
							})
						})
					})
				};
			}
			return 0;
		}

		private static int MapToJSON(out JObject jObject)
		{
			jObject = null;
			if (currentUser == Guid.Empty)
			{
				return 154;
			}
			jObject = new JObject {
				{
					"Branches",
					new JArray(Database.Branches.Select(branch => new JObject
					{
						{
							"ID",
							branch.GUID.ToString()
						},
						{
							"FleetManager",
							branch.FleetManager.GUID.ToString()
						},
						{
							"Fleets",
							new JArray(branch.Fleets.Select(fleet => new JObject
							{
								{
									"ID",
									fleet.GUID
								},
								{
									"Vehicles",
									new JArray(fleet.Vehicles.Select(vehicle => new JObject
									{
										{
											"ID",
											vehicle.GUID
										},
										{
											"Numberplate",
											vehicle.Numberplate
										},
										{
											"Category",
											(int)vehicle.Category
										}
									}))
								}
							}))
						}
					}))
				},
				{
					"Customers",
					new JArray(Database.Customers.Select(customer => new JObject
					{
						{
							"GUID",
							customer.GUID
						},
						{
							"ID",
							customer.ID
						},
						{
							"Name",
							customer.Name
						},
						{
							"Residence",
							customer.Residence
						},
						{
							"Bookings",
							new JArray(customer.Bookings.Select(booking => new JObject
							{
								{
									"Startdate",
									booking.Startdate.ToString("yyyyMMdd")
								},
								{
									"Enddate",
									booking.Enddate.ToString("yyyyMMdd")
								},
								{
									"GUID",
									booking.GUID.ToString()
								},
								{
									"Vehicle",
									booking.VehicleGuid.ToString()
								}
							}))
						}
					}))
				},
				{
					"FleetManagers",
					ReadDatabaseFile()["FleetManagers"]
				}
			};
			return 0;
		}

		internal static bool CheckUserCredentials(string username, string password)
		{
			IEnumerable<JToken> users = from user in ReadDatabaseFile()["FleetManagers"] where (string)user["Username"] == username && encoder.Compare(password, (string)user["Password"]) select user;
			return users.Count() != 1 ? false : Guid.TryParse((string)users.Single()["GUID"], out currentUser);
		}
	}
}