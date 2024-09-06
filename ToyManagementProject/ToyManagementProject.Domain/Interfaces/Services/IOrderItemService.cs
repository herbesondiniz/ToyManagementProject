using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IOrderItemService
	{
		Task<List<OrderItem>> GetAllAsync();
		Task<OrderItem> GetByIdAsync(int id);
		Task AddAsync(OrderItem obj);
		Task UpdateAsync(OrderItem obj);
		Task DeleteAsync(int id);
	}
}
