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
		
		private readonly List<string> _errorsNotifications = new List<string>();
		public IReadOnlyList<string> ErrorsNotifications => _errorsNotifications.AsReadOnly();

		public Toy(string name, string description, decimal price)
		{
			SetName(name);
			SetDescription(description);
			SetPrice(price);						
		}
		public void SetId(int id)
		{
			if (id <= 0)
			{
				_errorsNotifications.Add($"Id is required");
				return;
			}

			Id = id;
		}
		public void SetName(string name) 
		{
			if (string.IsNullOrWhiteSpace(name)) 
			{
				_errorsNotifications.Add($"Name is required");
				return;
			}

			Name = name;				
		}
		public void SetDescription(string description)
		{
			if (string.IsNullOrWhiteSpace(description))
			{
				_errorsNotifications.Add($"Description is required");
				return;
			}

			Description = description;
		}
		public void SetPrice(decimal price)
		{
			if(price <= 0) 
			{
				_errorsNotifications.Add($"Price must be greater than zero");
			}

			Price = price;
		}
		public void UpdateToy(int id, string name, string description, decimal price)
		{			
			SetId(id);
			SetName(name);
			SetDescription(description);
			SetPrice(price);			
		}
		public bool IsValid() 
		{
			return !_errorsNotifications.Any();
		}
	}
}
