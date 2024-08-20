
namespace ToyManagementProject.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public IEnumerable<OrderItem> Items { get; set; }
		public decimal TotalAmount 
		{ 
			get 
			{ 				
				return Items?.Sum(item => item.Quantity * item.Price) ?? 0;
			} 			
		}

		public bool IsValid(Order order)
		{
			return Items != null && Items.Any(x => x.OrderId == order.Id);
		}
	}
}
