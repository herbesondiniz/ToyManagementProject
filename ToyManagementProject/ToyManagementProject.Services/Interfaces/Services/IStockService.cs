using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IStockService
	{
		Task<Result<StockDto>> GetStockByToyIdAsync(int id);
		Task<Result<IList<StockDto>>> GetAllAsync();
		Task<Result<StockDto>> GetByIdAsync(int id);
		Task<Result<StockDto>> AddAsync(Stock obj);
		Task<Result<StockDto>> UpdateAsync(Stock stock);
		Task<Result<StockDto>> DeleteAsync(int id);
	}
}
