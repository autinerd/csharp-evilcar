using System;

namespace CsharpEvilcar.UserInterface
{
	internal static partial class UserInterface
	{
		internal static void Main()
		{
			// print programm begin info
			Prompt.Print(Prompt.General.EvilCarLogo);

			// login and run the prompt
			if (Login())
			{
				Prompt.Print(Prompt.Login.Successful);
				Prompt.Print(Prompt.General.RemindHelp);
				if (Database.DatabaseController.LoadDatabase().IsSuccess)
				{
					while (Prompt.Main.Execute()) { };
				}
			}
			else { Prompt.Print(Prompt.Login.Failed); }

			// close the programm
			Prompt.Print(Prompt.General.ProgrammEnd);
			Prompt.Print("", "");
			_ = Console.ReadKey();
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
