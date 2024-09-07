using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IStockRepository
	{
		Task<Stock> GetStockByToyIdAsync(int toyId);
		Task<List<Stock>> GetAllAsync();
		Task<Stock> GetByIdAsync(int id);
		Task AddAsync(Stock obj);
		Task UpdateAsync(Stock obj);
		Task DeleteAsync(int id);
	}
}
