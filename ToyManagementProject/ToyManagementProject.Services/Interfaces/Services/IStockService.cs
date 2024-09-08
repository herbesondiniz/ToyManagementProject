using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IStockService
	{
		Task<Stock> GetStockByToyIdAsync(int id);
		Task<List<Stock>> GetAllAsync();
		Task<Stock> GetByIdAsync(int id);
		Task<Result<Stock>> AddAsync(Stock obj);
		Task UpdateAsync(Stock obj);
		Task DeleteAsync(int id);
	}
}
