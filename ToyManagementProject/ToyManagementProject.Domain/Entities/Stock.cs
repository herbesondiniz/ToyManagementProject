
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Linq;

namespace ToyManagementProject.Domain.Entities
{
	public class Stock
	{
		[Key]
		public int Id { get; private set; }
		public int ToyId { get; private set; }
		public int Quantity { get; private set; }
		public IList<string>? ErrorsNotifications { get; private set; }
		public Stock(){}
        public Stock(int toyId, int quantity)
        {
			ToyId = toyId;
			Quantity = quantity;

			ValidationErrors();
        }
		public void DeductFromStock(int quantity) 
		{
			var notifications = new List<string>();
			if (quantity > Quantity) 
			{
				notifications.Add($"Quantity is bigger than current stock");
			}
			
			ErrorsNotifications = notifications;

			Quantity -= quantity;
		}
		public void ValidationErrors()
		{
			var notifications = new List<string>();

			if (ToyId <= 0)
				notifications.Add($"ToyId is required");
			

			if (Quantity <= 0)
				notifications.Add($"Quantity is required");
			
			ErrorsNotifications = notifications;
		}

	}
}
