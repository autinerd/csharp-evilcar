using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsharpEvilcar
{
	internal static class DatabaseController
	{
		internal static Database Database = null;
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
			if (currentUser == Guid.Empty)
			{
				return 154;
			}
			return 0;
		}

		internal static bool CheckUserCredentials(string username, string password)
		{
			JObject db = ReadDatabaseFile();
			IEnumerable<JToken> users = from user in db["Users"] where (string)user["Username"] == username && encoder.Compare(password, (string)user["Password"]) select user;
			return users.Count() != 1 ? false : Guid.TryParse((string)users.Single()["ID"], out currentUser);
		}
	}
}
