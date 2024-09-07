using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IOrderItemRepository
	{
		Task<List<OrderItem>> GetAllAsync();
		Task<OrderItem> GetByIdAsync(int id);
		Task AddAsync(OrderItem obj);
		Task UpdateAsync(OrderItem obj);
		Task DeleteAsync(int id);
	}
}
