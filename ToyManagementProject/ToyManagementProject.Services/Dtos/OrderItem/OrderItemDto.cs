using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyManagementProject.Services.Dtos.OrderItem
{
	public class OrderItemDto
	{
		public int Id { get; set; }
		public int ToyId { get; set; }
		public int Quantity { get; set; }
	}
}
