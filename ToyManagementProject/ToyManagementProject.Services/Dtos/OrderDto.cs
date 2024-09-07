
namespace ToyManagementProject.Services.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public List<OrderItemDto>? Items { get; set; }
		public decimal TotalAmount { get; set; }		
	}
}
