using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main(string[] args)
		{
			// print programm begin info
			Console.Write(OutputStrings.EvilCarLogo);
			Console.Write(OutputStrings.MainLevel.ProgrammBegin);

			// login and run the prompt
			if (Login())
			{
				Console.Write(OutputStrings.Login.Successful);
				ErrorCode loaded = Database.DatabaseController.LoadDatabase();
				Prompt();
			}
			else { Console.WriteLine(OutputStrings.Login.Failed); }

			// close the programm
			Console.Write(OutputStrings.MainLevel.ProgrammEnd);
			Console.ReadKey();
		}


		private static void Prompt()
		{
			while (true)
			{
				try
				{
					// read in command and separate selection from parameters
					Console.Write(OutputStrings.Prompt);
					string[] parameters = GetInput();

					string selection = parameters[0].ToLower();
					parameters = parameters.Skip(1).ToArray();


					// execute command
					switch (selection)
					{
						case "add":
							/*
							 * vehicle		<numberplate> <brand> <model> <category> <fleet_ID>
							 * customer		<name> <residence>
							 */
							AddCase(parameters);
							continue;

						case "edit":
							/*
							 * vehicle		<vehicle_ID> (numberplate|brand|model|category|fleet_ID) <new_value>
							 * customer		<customer_ID> (name|residence) <new_value>
							 */
							EditCase(parameters);
							continue;

						case "remove":
							/*
							 * vehicle		<vehicle_ID>
							 * customer		<customer_ID>
							 */
							RemoveCase(parameters);
							continue;

						case "view":
							/*
							 * branch	
							 * fleet		[<branch_ID>]
							 * vehicle		<none>
							 *				| <branch_ID> [<fleet_ID>]
							 *				| single <vehicle_ID>
							 *				
							 * customer		[ <customer_ID>]
							 * 
							 * bookings		<none>
							 *				| <branch_ID> [<fleet_ID>]
							 *				| vehicle <vehicle_ID>
							 *				| customer <customer_ID>
							 *				| booking <booking_ID>
							 * 
							 */
							ViewCase(parameters);
							continue;

						case "booking":
							/*
							 * rent <vehicle_ID> <customer_ID>
							 * return <vehicle_ID>
							 */
							BookCase(parameters);
							continue;

						case "?":
						case "help":
							Console.Write(OutputStrings.MainLevel.Help);
							continue;

						case "logout":
						case "exit":
							return;

						default:
							Console.WriteLine(OutputStrings.MainLevel.CommandNotExisting);
							continue;
					}
				}
				catch (AbortCommandExecution)
				{
					Console.Write(OutputStrings.MainLevel.CommandAbort);
					continue;
				}
			}
		}
	}
}
