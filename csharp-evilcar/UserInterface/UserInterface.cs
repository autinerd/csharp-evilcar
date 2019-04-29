using System;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main()
		{
			// print programm begin info
			Console.Write(Output.General.EvilCarLogo);

			// login and run the prompt
			if (Login())
			{
				Print(Output.Login.Successful);
				Print(Output.General.RemindHelp);
				if (Database.DatabaseController.LoadDatabase() == ReturnValue.Success)
				{
					Prompt();
				}
			}
			else { Print(Output.Login.Failed); }

			// close the programm
			Print(Output.General.ProgrammEnd);
			Print("", "");
			_ = Console.ReadKey();
		}


		private static void Prompt()
		{
			while (true)
			{
				ReturnValue.Type code = Output.Main.Execute(Array.Empty<string>());
				if (code is ReturnValue.PassReturnValue)
				{
					if (code == ReturnValue.Empty)
					{
						_ = Database.DatabaseController.SaveDatabase();
					}
					continue;
				}
				else if (code == ReturnValue.HelpNeeded)
				{
					Print(ReturnValue.Case.GetHelp + "\n" + Output.General.SyntaxHead + "\n" + ReturnValue.Case.GetSyntax);
					continue;
				}
				else if (code == ReturnValue.RequestedLogout)
				{
					break;
				}
				else
				{
					Print(code.Text);
				}
				
			}
		}
	}
}


/*
 * MainCase	SubCase		Parameter 1		Parameter 2				Parameter 3		Parameter 4		Parameter 5
 * 
 * add		vehicle		<numberplate>	<brand>					<model>			<category>		<fleet_ID>
 * add		customer	<name>			<residence>
 *			
 * edit		vehicle		<vehicle_ID>	{numberplate|fleet_ID}	<new_value>
 * edit		customer	<customer_ID>	{name|residence}		<new_value>
 *			
 * delete	vehicle		<vehicle_ID>
 * delete	customer	<customer_ID>
 *			
 * view		branch
 * view		fleet
 *						<branch_ID>
 * view		vehicle
 *						<branch_ID>		[<fleet_ID>]
 *						single			<vehicle_ID>
 * view		customer
 *						<customer_ID>
 * view		bookings
 *						<branch_ID>		[<fleet_ID>]
 *						vehicle			<vehicle_ID>
 *						customer		<customer_ID>
 *						booking			<booking_ID>
 *
 * booking	rent		<vehicle_ID>	<customer_ID>
 * booking	return		<vehicle_ID>
 * 
 * logout
 */
