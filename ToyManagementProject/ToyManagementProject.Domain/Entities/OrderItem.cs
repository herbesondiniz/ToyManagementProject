
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToyManagementProject.Domain.Entities
{
	public class OrderItem
	{
		[Key]
		public int Id { get; private set; }
		public int ToyId { get; private set; }
		public int OrderId { get; set; }
		public int Quantity { get; private set; }
		public decimal Price { get; set; }
		public IList<string>? ErrorsNotifications { get; private set; }

		public OrderItem(int toyId, int orderId, int quantity, decimal price)
		{
			ToyId = toyId;
			OrderId = orderId;
			Quantity = quantity;
			Price = price;

			ValidationErrors();
		}
		public void AddPrice(decimal price)
		{
			Price = price;
		}
		public void AddOrderId(int orderId)
		{
			OrderId = orderId;
		}		

		public void ValidationErrors()
		{
			var notifications = new List<string>();

			if (ToyId <= 0)
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
