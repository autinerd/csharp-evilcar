
using CsharpEvilcar.Prompt;
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
		internal static Database DatabaseObject { get; private set; } = null;

		/// <summary>
		/// The current logged in user.
		/// </summary>
		internal static Guid CurrentUser
		{
			get => currentUser;
			private set => currentUser = value;
		}
		private static Guid currentUser = Guid.Empty;

		internal static readonly Scrypt.ScryptEncoder encoder = new Scrypt.ScryptEncoder();

		/// <summary>
		/// Loads the database file contents into the <see cref="DatabaseObject"/> object.
		/// </summary>
		/// <returns>Error code</returns>
		internal static ReturnValue LoadDatabase()
		{
			try
			{
				return MapToDatabase(ReadDatabaseFile());
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsDatabaseError);
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}

		/// <summary>
		/// Saves the <see cref="DatabaseObject"/> object into the database file.
		/// </summary>
		/// <returns></returns>
		internal static ReturnValue SaveDatabase()
		{
			try
			{
				return SaveDatabaseFile();
			}
#pragma warning disable CA1031 // Do not catch general exception types
			catch (Exception)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsDatabaseError);
			}
#pragma warning restore CA1031 // Do not catch general exception types
		}

		private static ReturnValue SaveDatabaseFile()
		{
			(ReturnValue returnval, JObject jObject) = MapToJSON();
			if (returnval == ErrorCodeFlags.IsError)
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
				return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
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

		private static ReturnValue MapToDatabase(JObject jObject)
		{
			if (CurrentUser == Guid.Empty)
			{
				return ReturnValue.GetValue(ErrorCodeFlags.IsNoUserLoggedIn);
			}
			else
			{
				DatabaseObject = new Database
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
							Enddate = (string)booking["Enddate"] == "default"
									? default
									: DateTime.ParseExact((string)booking["Enddate"], "yyyyMMdd", null),
							VehicleID = (int)booking["Vehicle"]
						}).ToList()
					}).ToList(),
					Branches = jObject["Branches"].Select((branch) => new DataClasses.Branch
					{
						GUID = Guid.Parse((string)branch["GUID"]),
						Location = (string)branch["Location"],
						FleetManager = new DataClasses.FleetManager
						{
							GUID = Guid.Parse((string)branch["FleetManager"]),
							Name = (string)( from f in jObject["FleetManagers"]
											 where (string)f["GUID"] == (string)branch["FleetManager"]
											 select f ).Single()["Name"],
							Residence = (string)( from f in jObject["FleetManagers"]
												  where (string)f["GUID"] == (string)branch["FleetManager"]
												  select f ).Single()["Residence"]
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
			return ReturnValue.GetValue(ErrorCodeFlags.IsSuccess);
		}

		private static (ReturnValue, JObject) MapToJSON() => currentUser == Guid.Empty
				? (ReturnValue.GetValue(ErrorCodeFlags.IsNoUserLoggedIn), null)
				: (ReturnValue.GetValue(ErrorCodeFlags.IsSuccess), new JObject {
				{
					"Branches",
					new JArray(DatabaseObject.Branches.Select(branch => new JObject
					{
						{
							"GUID",
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
									"GUID",
									fleet.GUID
								},
								{
									"Vehicles",
									new JArray(fleet.Vehicles.Select(vehicle => new JObject
									{
										{
											"GUID",
											vehicle.GUID
										},
										{
											"Numberplate",
											vehicle.Numberplate
										},
										{
											"Category",
											(int)vehicle.Category
										},
										{
											"Model",
											vehicle.Model
										},
										{
											"Brand",
											vehicle.Brand
										},
										{
											"VehicleID",
											vehicle.VehicleID
										}
									}))
								},
								{
									"Location",
									fleet.Location
								}
							}))
						},
						{
							"Location",
							branch.Location
						}
					}))
				},
				{
					"Customers",
					new JArray(DatabaseObject.Customers.Select(customer => new JObject
					{
						{
							"GUID",
							customer.GUID
						},
						{
							"CustomerID",
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
									booking.Enddate == default
									? "default"
									: booking.Enddate.ToString("yyyyMMdd", CultureInfo.InvariantCulture)
								},
								{
									"GUID",
									booking.GUID.ToString()
								},
								{
									"Vehicle",
									booking.VehicleID
								},
								{
									"BookingID",
									booking.BookingID
								}
							}))
						}
					}))
				},
				{
					"FleetManagers",
					ReadDatabaseFile()["FleetManagers"]
				}
			});

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
