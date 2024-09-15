using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyManagementProject.Services.Dtos.Stock
{
	public class CreateStockDto
	{
		public int ToyId { get; set; }
		public int Quantity { get; set; }
	}
}
