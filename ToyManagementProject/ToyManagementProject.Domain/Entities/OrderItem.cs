
using System.ComponentModel.DataAnnotations;

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

        public OrderItem(int toyId,int quantity)
        {
			ToyId = toyId;
			Quantity = quantity;			
        }
    }
}
