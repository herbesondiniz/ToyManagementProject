using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Validators.Interfaces;

namespace ToyManagementProject.Domain.Validators
{
	public class ToyValidator: IValidator<Toy>
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
