using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class StockRepository : IStockRepository
	{
		private readonly IRepositoryBase<Stock> _repository;
        public StockRepository(IRepositoryBase<Stock> repository)
        {
			_repository = repository;			
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
			var stocks = await _repository.GetAllAsync();
			
			return stocks.FirstOrDefault(x => x.ToyId == toyId);			
		}

		public async Task UpdateAsync(Stock obj)
		{
			await _repository.UpdateAsync(obj);	
		}
	}
}
