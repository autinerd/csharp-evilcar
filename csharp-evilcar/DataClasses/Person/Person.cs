namespace CsharpEvilcar.DataClasses
{
	internal class Person : GuidObject
	{
		public string Name { get; set; }
		public string Residence { get; set; }
		public Person(string name, string residence) : base()
		{
			Name = name;
			Residence = residence;
		}
		public Person() : base() { }
	}
}
