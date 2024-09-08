
using System.ComponentModel.DataAnnotations;

namespace ToyManagementProject.Domain.Entities
{
	public class Order
	{
		[Key]
		public int Id { get; private set; }
		public int ClientId { get; private set; }
		public IEnumerable<OrderItem> Items { get; private set; }		
		public decimal TotalAmount => Items.Sum(item => item.Quantity * item.Price);
		public IList<string>? ErrorsNotifications { get; private set; }
		public Order()
		{
			Items = new List<OrderItem>();
			ErrorsNotifications = new List<string>();
		}
		public Order(int clientId, IEnumerable<OrderItem> items)
		{						
			ClientId = clientId;						

 			Items = items ?? new List<OrderItem>();

			ValidationErrors();
		}

		public void AddItems(IEnumerable<OrderItem> items) 
		{			
			Items = items ?? new List<OrderItem>();
			
			ValidationErrors();
		} 
		public void ValidationErrors() 
		{
			var notifications = new List<string>();

			if(ClientId <= 0)
				notifications.Add($"ClientId is required");

			if (!Items.Any())
			{
				notifications.Add("Order must have at least one item.");
			}
			else
			{
				foreach (var item in Items)
				{
					if (item.Quantity <= 0)
					{
						notifications.Add($"Item {item.Toy.Name} must have a quantity greater than zero.");
					}
				}
			}
			ErrorsNotifications = notifications;
		}
	}
}
