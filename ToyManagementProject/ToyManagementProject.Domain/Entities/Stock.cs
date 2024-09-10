
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace ToyManagementProject.Domain.Entities
{
	public class Stock
	{
		[Key]
		public int Id { get; private set; }
		public int ToyId { get; private set; }
		public Toy? Toy { get; private set; }
		public int Quantity { get; private set; }
		
		private readonly List<string> _errorsNotifications = new List<string>();
		public IReadOnlyList<string> ErrorsNotifications => _errorsNotifications.AsReadOnly();		
        public Stock(int toyId, int quantity)
        {
			SetToyId(toyId);
			SetQuantity(quantity);			
        }
		public void SetToyId(int toyId) 
		{
			if(toyId <= 0) 
			{
				_errorsNotifications.Add("toyId is required");
				return;
			}

			ToyId = toyId;
		}
		public void SetToy(Toy toy)
		{
			if(toy == null) 
			{
				_errorsNotifications.Add("Toy is empty");
				return;
			}

			Toy = toy;
		}
		public void SetQuantity(int quantity)
		{
			if (quantity <= 0)
			{
				_errorsNotifications.Add("quantity is required");
				return;
			}

			Quantity = quantity;
		}
		public void DeductFromStock(int quantity) 
		{
			if (quantity <= 0)
			{
				_errorsNotifications.Add($"Quantity must be bigger than 0");
				return;
			}

			if (quantity > Quantity) 
			{
				_errorsNotifications.Add($"Quantity is bigger than current stock");
				return;
			}						

			Quantity -= quantity;			
		}
		public bool IsValid() 
		{
			return !_errorsNotifications.Any();
		}
	}
}
