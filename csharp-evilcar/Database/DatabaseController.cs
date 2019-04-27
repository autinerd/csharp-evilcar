using CsharpEvilcar.UserInterface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsharpEvilcar.Database
{
	/// <summary>
	/// The DatabaseController controls the communication between the Database object and the database file.
	/// </summary>
	internal static class DatabaseController
	{
		/// <summary>
		/// The database object which represents the contents of the database file.
		/// </summary>
		internal static Database Database { get; private set; } = null;

		/// <summary>
		/// The current logged in user.
		/// </summary>
		internal static Guid CurrentUser
		{
			get => currentUser;
			private set => currentUser = value;
		}
		private static Guid currentUser = Guid.Empty;

		private static readonly Scrypt.ScryptEncoder encoder = new Scrypt.ScryptEncoder();

		/// <summary>
		/// Loads the database file contents into the <see cref="Database"/> object.
		/// </summary>
		/// <returns>Error code</returns>
		internal static ErrorCode LoadDatabase()
		{
			try
			{
				return MapToDatabase(ReadDatabaseFile());
			}
			catch (Exception)
			{
				return ErrorCode.DatabaseError;
			}
		}

		/// <summary>
		/// Saves the <see cref="Database"/> object into the database file.
		/// </summary>
		/// <returns></returns>
		internal static ErrorCode SaveDatabase()
		{
			try
			{
				return SaveDatabaseFile();
			}
			catch (Exception)
			{
				return ErrorCode.DatabaseError;
			}
		}

		private static ErrorCode SaveDatabaseFile()
		{
			ErrorCode returnval = MapToJSON(out JObject jObject);
			if (returnval != 0)
			{
				return returnval;
			}
			try
			{
#pragma warning disable CA1508 // Avoid dead conditional code
				using (JsonTextWriter writer = new JsonTextWriter(new StreamWriter("database.json")))
#pragma warning restore CA1508 // Avoid dead conditional code
				{
					jObject.WriteTo(writer);
				}
				return ErrorCode.Success;
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

		private static ErrorCode MapToDatabase(JObject jObject)
		{
			if (CurrentUser == Guid.Empty)
			{
				return ErrorCode.NoUserLoggedIn;
			}
			else
			{
				Database = new Database
				{
					Customers = jObject["Customers"].Select((customer) => new DataClasses.Customer(true)
					{
						GUID = Guid.Parse((string)customer["GUID"]),
						CustomerID = (int)customer["CustomerID"],
						Name = (string)customer["Name"],
						Residence = (string)customer["Residence"],
						Bookings = customer["Bookings"].Select((booking) => new DataClasses.Booking(true)
						{
							GUID = Guid.Parse((string)booking["GUID"]),
							BookingID = (int)booking["BookingID"],
							Startdate = DateTime.ParseExact((string)booking["Startdate"], "yyyyMMdd", null),
							Enddate = DateTime.ParseExact((string)booking["Enddate"], "yyyyMMdd", null),
							VehicleID = (int)booking["Vehicle"]
						}).ToList()
					}).ToList(),
					Branches = jObject["Branches"].Select((branch) => new DataClasses.Branch
					{
						GUID = Guid.Parse((string)branch["GUID"]),
						FleetManager = new DataClasses.FleetManager
						{
							GUID = Guid.Parse((string)branch["FleetManager"])
#warning Fleet Manager Data
						},
						Fleets = branch["Fleets"].Select((fleet) => new DataClasses.Fleet
						{
							GUID = Guid.Parse((string)fleet["GUID"]),
							Location = (string)fleet["Location"],
							Vehicles = fleet["Vehicles"].Select<JToken, DataClasses.Vehicle>((vehicle) =>
							{
								switch ((DataClasses.Vehicle.CategoryEnum)(int)vehicle["Category"])
								{
									case DataClasses.Vehicle.CategoryEnum.Small:
										return new DataClasses.SmallVehicle((string)vehicle["Numberplate"], (string)vehicle["Model"], (string)vehicle["Brand"], true)
										{
											GUID = Guid.Parse((string)vehicle["GUID"]),
											VehicleID = (int)vehicle["VehicleID"]
										};
									case DataClasses.Vehicle.CategoryEnum.Midsize:
										return new DataClasses.MidsizeVehicle((string)vehicle["Numberplate"], (string)vehicle["Model"], (string)vehicle["Brand"], true)
										{
											GUID = Guid.Parse((string)vehicle["GUID"]),
											VehicleID = (int)vehicle["VehicleID"]
										};
									case DataClasses.Vehicle.CategoryEnum.Large:
										return new DataClasses.LargeVehicle((string)vehicle["Numberplate"], (string)vehicle["Model"], (string)vehicle["Brand"], true)
										{
											GUID = Guid.Parse((string)vehicle["GUID"]),
											VehicleID = (int)vehicle["VehicleID"]
										};
									case DataClasses.Vehicle.CategoryEnum.Electric:
										return new DataClasses.ElectricVehicle((string)vehicle["Numberplate"], (string)vehicle["Model"], (string)vehicle["Brand"], true)
										{
											GUID = Guid.Parse((string)vehicle["GUID"]),
											VehicleID = (int)vehicle["VehicleID"]
										};
									default:
										return null;
								}
							}).ToList()
						}).ToList()
					}).ToList()
				};
			}
			return ErrorCode.Success;
		}

		private static ErrorCode MapToJSON(out JObject jObject)
		{
			jObject = null;
			if (currentUser == Guid.Empty)
			{
				return ErrorCode.NoUserLoggedIn;
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
							customer.CustomerID
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
									booking.Startdate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
								},
								{
									"Enddate",
									booking.Enddate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
								},
								{
									"GUID",
									booking.GUID.ToString()
								},
								{
									"Vehicle",
									booking.VehicleID
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
			return ErrorCode.Success;
		}

		/// <summary>
		/// Checks if the given <paramref name="username"/> is in the database and the given <paramref name="password"/> is correct.
		/// </summary>
		/// <param name="username">The username to check against</param>
		/// <param name="password">The password to check against</param>
		/// <returns>True when succeeded, False when failed</returns>
		internal static bool CheckUserCredentials(string username, string password)
		{
			IEnumerable<JToken> users = from user in ReadDatabaseFile()["FleetManagers"]
										where (string)user["Username"] == username && encoder.Compare(password, (string)user["Password"])
										select user;
			return users.Count() != 1
				? false
				: Guid.TryParse((string)users.Single()["GUID"], out currentUser);
		}
	}
}
