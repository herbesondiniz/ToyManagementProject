using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Services.Dtos
{
	public class StockDto
	{
		public int Id { get; set; }
		public int ToyId { get; set; }				
		public int Quantity { get; set; }		
	}
}
