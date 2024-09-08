
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToyManagementProject.Domain.Entities
{
	public class OrderItem
	{
		[Key]
		public int Id { get; private set; }
        public int ToyId { get; private set; }
        public Toy Toy { get; private set; }
		public int OrderId { get; private set; }
		public int Quantity { get; private set; }
		public decimal Price { get; private set; }

		private readonly List<string> _errorsNotifications = new List<string>();
		public IReadOnlyList<string> ErrorsNotifications => _errorsNotifications.AsReadOnly();		
        public OrderItem(){}
        public OrderItem(Toy toy, int orderId, int quantity, decimal price)
		{
			SetToy(toy);
			SetOrderId(orderId);
			SetQuantity(quantity);
			SetPrice(price);		
		}
		public void SetToy(Toy toy)
		{
			if (toy == null)
			{
				_errorsNotifications.Add($"Toy is empty");
				return;
			}

			Toy = toy;
			ToyId = toy.Id;
			Price = toy.Price;
		}
		public void SetOrderId(int orderId)
		{

			if (orderId <= 0)
			{
				_errorsNotifications.Add($"OrderId is required");
				return;
			}

			OrderId = orderId;
		}
		public void SetQuantity(int quantity)
		{
			if (quantity <= 0)
			{
				_errorsNotifications.Add("Quantity must be greater than zero");
				return;
			}

			Quantity = quantity;
		}
		public void SetPrice(decimal price)
		{
			if(price < 0) 
			{
				_errorsNotifications.Add($"Price must be greater than zero");
				return;
			}

			Price = price;
		}
		public bool IsValid() 
		{
			return !_errorsNotifications.Any();
		}
	}
}
