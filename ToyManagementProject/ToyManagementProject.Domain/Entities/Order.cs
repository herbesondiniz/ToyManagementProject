
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
		
		private readonly List<string> _errorsNotifications = new List<string>();
		public IReadOnlyList<string> ErrorsNotifications => _errorsNotifications.AsReadOnly();
		public Order()
		{
			Items = new List<OrderItem>();			
		}
		public Order(int clientId, IEnumerable<OrderItem> items)
		{
			SetClient(clientId);					
			AddItems(items ?? new List<OrderItem>());
		}

		public void SetClient(int clientId)		
		{
			if (clientId <= 0) 
			{
				_errorsNotifications.Add($"The Client is required");
				return;
			}
			ClientId = clientId;			
		}

		public void AddItems(IEnumerable<OrderItem> items) 
		{
			if (!items.Any())
			{
				_errorsNotifications.Add("Order must have at least one item.");
				return;
			}			
			else
			{
				foreach (var item in items)
				{
					if(items.Count() == 1 && item.ToyId == 0) 
					{
						_errorsNotifications.Add($"At least 1 Item is required.");
						return;
					}
					if (item.Quantity <= 0)
					{
						_errorsNotifications.Add($"Item {item.Toy.Name} must have a quantity greater than zero.");
						return;
					}
				}
			}
			Items = items;						
		} 	
		public bool IsValid() 
		{
			return !_errorsNotifications.Any();
		}
	}
}
