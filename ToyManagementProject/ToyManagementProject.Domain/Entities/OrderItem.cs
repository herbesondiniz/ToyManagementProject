
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
		public int OrderId { get; set; }
		public int Quantity { get; private set; }
		public decimal Price { get; set; }
		public IList<string>? ErrorsNotifications { get; private set; }
        public OrderItem(){}
        public OrderItem(Toy toy, int orderId, int quantity, decimal price)
		{
			Toy = toy;
			OrderId = orderId;
			Quantity = quantity;
			Price = price;

			ValidationErrors();
		}		
		public void AddPrice(decimal price)
		{
			var notifications = new List<string>();

			if(price < 0) 
			{
				notifications.Add($"Price is required");				
			}

			ErrorsNotifications = notifications;

			Price = price;
		}
		public void SetToy(Toy toy)
		{
			var notifications = new List<string>();

			if (toy == null) 
			{
				notifications.Add($"Toy is empty");
			}					

			Toy = toy;
			ToyId = toy.Id;
			Price = toy.Price;

			ErrorsNotifications = notifications;
		}
		public void AddOrderId(int orderId)
		{
			var notifications = new List<string>();

			if (orderId < 0)
			{
				notifications.Add($"OrderId is required");
			}			

			OrderId = orderId;

			ErrorsNotifications = notifications;
		}		

		public void ValidationErrors()
		{
			var notifications = new List<string>();

			if (Toy == null || Toy.Id <= 0)
				notifications.Add($"ToyId is required");

			if (OrderId <= 0)
				notifications.Add($"OrderId is required");

			if (Quantity <= 0)
				notifications.Add($"Quantity is required");
			
			if (Price <= 0)
				notifications.Add($"Price is required");

			ErrorsNotifications = notifications;
		}
	}
}
