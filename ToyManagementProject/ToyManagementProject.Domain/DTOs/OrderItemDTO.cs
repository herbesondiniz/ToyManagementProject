using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyManagementProject.Domain.DTOs
{
	public class OrderItemDTO
	{
		public int ToyId { get; set; }
		public int Quantity { get; set; }
	}
}
