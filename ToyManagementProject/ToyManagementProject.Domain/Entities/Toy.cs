using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace ToyManagementProject.Domain.Entities
{
	public class Toy
	{
		[Key]
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }
		public decimal Price { get; private set; }			
		public IList<string>? ErrorsNotifications { get; private set; }       

		public Toy(string name, string description, decimal price)
		{
			Name = name;
			Description = description;
			Price = price;			

			ValidationErrors();
		}
		public void UpdateToy(int id, string name, string description, decimal price)
		{
			Id = id;
			Name = name;
			Description = description;
			Price = price;		

			ValidationErrors();
		}
		public void ValidationErrors() 
		{
			var notifications = new List<string>();

			if (string.IsNullOrWhiteSpace(Name))
				notifications.Add($"Name is required");

			if (string.IsNullOrWhiteSpace(Description))
				notifications.Add($"Description is required");

			if (Price <= 0)
				notifications.Add($"Price is required");

			ErrorsNotifications = notifications;
		}
	}
}
