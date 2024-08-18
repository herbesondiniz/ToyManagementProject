
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class StockService : IStockService
	{
		private readonly IRepositoryBase<Stock> _repository;
        public StockService(IRepositoryBase<Stock> repositoryBase)
        {
			_repository = repositoryBase;

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
		public async Task UpdateAsync(Stock obj)
		{
			await (_repository.UpdateAsync(obj));	
		}
	}
}
