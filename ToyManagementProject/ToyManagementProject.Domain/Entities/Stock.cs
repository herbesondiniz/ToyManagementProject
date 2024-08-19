
using System.ComponentModel.DataAnnotations;

namespace ToyManagementProject.Domain.Entities
{
	public class Stock
	{
		[Key]
		public int Id { get; set; }
		public int ToyId { get; set; }
		public int Quantity { get; set; }
	}
}
