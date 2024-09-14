using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IOrderRepository
	{
		Task<List<Order>> GetAllAsync(Func<IQueryable<Order>, IQueryable<Order>> include = null);
		Task<Order> GetByIdAsync(int id, Func<IQueryable<Order>, IQueryable<Order>> include = null);
		Task AddAsync(Order obj);
		Task UpdateAsync(Order obj);
		Task DeleteAsync(int id);
	}
}
