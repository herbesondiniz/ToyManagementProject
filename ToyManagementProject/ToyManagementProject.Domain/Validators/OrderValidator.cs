using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Validators.Interfaces;

namespace ToyManagementProject.Domain.Validators
{
	public class OrderValidator : IValidator<Order>
	{
		public IEnumerable<string> Validate(Order order)
		{
			var _notifications = new List<string>();

			if (order.ClientId <= 0)
			{
				_notifications.Add("ClientId is required.");
			}

			if (!order.Items.Any())
			{
				_notifications.Add("Order must have at least one item.");
			}
			else
			{
				foreach (var item in order.Items)
				{
					if (item.Quantity <= 0)
					{
						_notifications.Add($"Item with ToyId {item.ToyId} must have a quantity greater than zero.");
					}
				}
			}
			return _notifications;
		}
	}
}
