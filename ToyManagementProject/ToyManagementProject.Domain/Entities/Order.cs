
namespace ToyManagementProject.Domain.Entities
{
	public class Order
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public List<OrderItem> Items { get; set; }
		public decimal TotalAmount { get; set; }
	}
}
