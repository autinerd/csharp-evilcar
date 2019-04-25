using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CsharpEvilcar
{
	partial class InternalLogic
	{
		int AddVehicle(string numberplate, string brand, string model, string category, uint fleet)
		{
			if (!Regex.IsMatch(numberplate, "^[A-Z]{1,3}-[A-Z]{1,2}-[0-9]{1,4}$"))
			{
				return 2;
			}
			switch (category.ToLower())
			{
				case "small":

				default:
					return 3;
			}
		}
	}
}
