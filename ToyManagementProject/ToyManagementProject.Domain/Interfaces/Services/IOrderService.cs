using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IOrderService : IServiceBase<Order>
	{
		Task<bool> ProcessOrderAsync(Order order);
	}
}
