using System;
using System.Collections.Generic;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Validators.Interfaces;

namespace ToyManagementProject.Services.Validators
{
	public class ToyValidator : IValidator<Toy>
	{
		public IEnumerable<string> Validate(Toy toy)
		{
			var _notifications = new List<string>();

			if (string.IsNullOrEmpty(toy.Name))
			{
				_notifications.Add("Name is required");
			}

			if (string.IsNullOrEmpty(toy.Description))
			{
				_notifications.Add("Description is required");
			}

			if (toy.Price <= 0)
			{
				_notifications.Add("Price is required");
			}

			return _notifications.AsReadOnly();
		}
	}
}
