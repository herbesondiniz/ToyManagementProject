
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class StockService : IStockService
	{
		private readonly IRepositoryBase<Stock> _repository;
		private readonly IStockRepository _stockRepository;
        public StockService(IRepositoryBase<Stock> repositoryBase, IStockRepository stockRepository)
        {
			_repository = repositoryBase;
			_stockRepository = stockRepository;	
		}
        public async Task AddAsync(Stock obj)
		{
			await _repository.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);	
		}
		public async Task<List<Stock>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Stock> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Stock> GetStockByToyIdAsync(int toyId)
		{
			return await _stockRepository.GetStockByToyIdAsync(toyId);
		}

		public async Task UpdateAsync(Stock obj)
		{
			await (_repository.UpdateAsync(obj));	
		}
	}
}
