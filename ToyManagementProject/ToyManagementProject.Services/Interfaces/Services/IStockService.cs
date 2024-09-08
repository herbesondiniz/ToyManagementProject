using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IStockService
	{
		Task<Stock> GetStockByToyIdAsync(int id);
		Task<Result<IList<StockDto>>> GetAllAsync();
		Task<Stock> GetByIdAsync(int id);
		Task<Result<StockDto>> AddAsync(Stock obj);
		Task UpdateAsync(Stock obj);
		Task DeleteAsync(int id);
	}
}
