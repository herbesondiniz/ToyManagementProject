
namespace ToyManagementProject.Domain.DTOs
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public List<OrderItemDTO>? Items { get; set; }
		public decimal TotalAmount { get; set; }		
	}
}
