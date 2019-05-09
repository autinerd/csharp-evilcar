

//#pragma warning disable IDE1006

namespace CsharpEvilcar
{
	internal static class Cases
	{
		internal static readonly Prompt.CaseTypes.Main Main = new Prompt.CaseTypes.Main()
		{
			CaseName = "main",
			SubCases = new Prompt.CaseTypes.Base[] {
				new Prompt.CaseTypes.Selection(){
					CaseName = "add",
					Help = "Use this if you want to add a vehicle or a customer to the databese.",
					AskForParameters = "Do you want to add a 'vehicle' or a 'costumer':",
					SubCases = new Prompt.CaseTypes.Base[] {
						new Prompt.CaseTypes.Command(){
							CaseName = "vehicle",
							AskForParameters =
								"Please enter now the parameters of the new vehicle in the following format:\n" +
								"numberplate brand model class fleetNr for example: S-XY-4589 audi q5 electric 3",
							Help = "Use this if you want to add a new vehicle.",
							Syntax = "add\t\tvehicle\t\t<numberplate>\t<brand>\t\t\t<model>\t\t<category>\t\t<fleet_ID>",
							ParameterLength = new int[]{5},
							SubFunction = InternalLogic.AddVehicle,
						},
						new Prompt.CaseTypes.Command(){
							CaseName = "customer",
							AskForParameters = "Please enter your <name> and <residence>",
							Help = "Use this if you want to add a new customer.",
							Syntax = "add\t\tcustomer\t<name>\t\t<residence>",
							ParameterLength = new int[]{2},
							SubFunction = InternalLogic.AddCustomer
						}
					}
				}, // add
				new Prompt.CaseTypes.Selection(){
					CaseName = "edit",
					AskForParameters="Do you want to edit a 'vehicle' or a 'customer'?",
					Help="Use this if you want to edit a vehicle or a customer.",
					SubCases = new Prompt.CaseTypes.Base[]{
						new Prompt.CaseTypes.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Please enter the <vehicle_ID>  'numberplate' or 'fleet_ID' and <new_value>.",
							Help="Use this if you want to edit a vehicle.",
							Syntax = "edit\tvehicle\t\t<vehicle_ID>\t{numberplate|fleet_ID}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_Id> 'name' or 'residence' and <new_value>.",
							Help="Use this if you want to edit a customer.",
							Syntax = "edit\tcustomer\t<customer_ID>\t{name|residence}\t<new_value>",
							ParameterLength = new int[]{3},
							SubFunction = InternalLogic.EditCustomer,
						},
					}
				}, // edit
				new Prompt.CaseTypes.Selection(){
					CaseName = "delete",
					AskForParameters = "Do you want to delete a 'vehicle' or a 'customer'?",
					Help="Use this if you want something to be deleted from the database",
					SubCases = new Prompt.CaseTypes.Base[]{
						new Prompt.CaseTypes.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Please enter the <vehicle_ID> of the vehicle you want do delete.",
							Help="Use this command if you want to delete a vehicle from the database."
,							Syntax = "delete\tvehicle\t\t<vehicle_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteVehicle,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_id> of the customer you want to delete.",
							Help="Use this command if you want to delete a customer from the database.",
							Syntax = "delete\tcustomer\t<customer_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.DeleteCustomer,
						},
					}
				}, // delete
				new Prompt.CaseTypes.Selection(){
					CaseName = "view",
					AskForParameters="Please enter if you want to view 'branch', 'fleet', 'vehicle', 'customer' or 'bookings'.",
					Help="Use this if you want to view data from the database.",
					SubCases = new Prompt.CaseTypes.Base[]{
						new Prompt.CaseTypes.Command()
						{
							CaseName = "branch",
							AskForParameters="Please enter 'all' if you want to see all or enter <banch_ID> if you only want so see one branch.",
							Help="Use this command if you want to view all ",
							Syntax = "view\tbranch\t\tall\n\t\t\t\t<branch_ID>",
							ParameterLength = new int[]{1},
							SubFunction=InternalLogic.ViewBranch,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "fleet",
							AskForParameters="Please enter nothing if you want to view all fleets or enter the <branch_ID> for which to see all fleets.",
							Help="Use this command if you want do view all all fleets or the fleets of one branch.",
							Syntax = "view\tfleet\t\tall\n\t\t\t\t<branch_ID>\tall\n\t\t\t\t<branch_ID>\t<fleet_ID>",
							ParameterLength = new int[]{1,2},
							SubFunction=InternalLogic.ViewFleet,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "vehicle",
							AskForParameters="Plase enter <branch_ID> and optional <fleet_ID> if you want to view all vehicle of a branch or a fleet in a branch.\n"+
							"If you want to see a single car, please enter 'single' and than the <vehicle_ID>.",
							Help="Use this command if you want to view one or many vehicles.",
							Syntax = "view\tvehicle\t\tall\n\t\t\t\tsingle\t\t<vehicle_ID>\n\t\t\t\t<branch_ID>\t<fleet_ID>\n\t\t\t\t<branch_ID>\tall",
							ParameterLength = new int[]{1,2},
							SubFunction=InternalLogic.ViewVehicle,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "customer",
							AskForParameters="Please enter the <customer_ID> of the customer you want to see or 'all' for all customer.",
							Help="Use this command if you want to view a customers data.",
							Syntax = "view\tcustomer\tall\n\t\t\t\t<customer_ID>",
							ParameterLength = new int[]{1},
							SubFunction= InternalLogic.ViewCustomer,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "bookings",
							AskForParameters="Fehlt noch.",
#warning view-booking AskForParameters falsche Übergabeparameter
							Help="",
#warning view-booking Help fehlt noch
							Syntax = "view\tbookings\n\t\t\t\t<branch_ID>\n\t\t\t\tfleet\t\t<fleet_ID>\n\t\t\t\tcustomer\t<customer_ID>\n\t\t\t\tbooking\t\t<booking_ID>",
							ParameterLength = new int[]{1,2},
							SubFunction=InternalLogic.ViewBooking,
						},
						new Prompt.CaseTypes.Command()
						{
							// just for set up our database
							CaseName = "password",
							AskForParameters = "Password to hash",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.ViewPassword
						}
					}
				}, // view
				new Prompt.CaseTypes.Selection(){
					CaseName = "booking",
					AskForParameters="Please enter if you want to 'rent' or 'return' a vehicle.",
					Help="Use this if you want to rent or return a vehicle.",
					SubCases = new Prompt.CaseTypes.Base[]{
						new Prompt.CaseTypes.Command()
						{
							CaseName = "rent",
							AskForParameters="Please enter the <vehicle_ID> and the <customer_ID> for a new booking.",
							Help="",
							Syntax = "booking\trent\t\t<vehicle_ID>\t<customer_ID>",
							ParameterLength = new int[]{1,2},
							SubFunction = InternalLogic.BookingRent,
						},
						new Prompt.CaseTypes.Command()
						{
							CaseName = "return",
							Syntax = "booking\treturn\t\t<vehicle_ID>",
							ParameterLength = new int[]{1},
							SubFunction = InternalLogic.BookingReturn,
						},
					}
				}, // booking
				new Prompt.CaseTypes.Logout(){
					CaseName = "logout",
					Syntax = "logout",
				}		//  logout
			},
		};
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
