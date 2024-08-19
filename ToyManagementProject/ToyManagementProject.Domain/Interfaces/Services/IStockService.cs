using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IStockService: IServiceBase<Stock>
	{
		Task<Stock> GetStockByToyIdAsync(int id);
	}
}
