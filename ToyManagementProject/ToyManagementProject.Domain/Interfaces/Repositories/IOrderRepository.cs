using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IOrderRepository
	{
		Task<List<Order>> GetAllAsync();
		Task<Order> GetByIdAsync(int id);
		Task AddAsync(Order obj);
		Task UpdateAsync(Order obj);
		Task DeleteAsync(int id);
	}
}
