using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class StockRepository : IStockRepository
	{		
		private readonly ToyDbContext _context;		
		public StockRepository(ToyDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Stock obj)
		{
			await _context.Set<Stock>().AddAsync(obj);
		}

		public Task DeleteAsync(int id)
		{			
			var obj = GetByIdAsync(id).Result;
			if (obj != null)
			{
				_context.Set<Stock>().Remove(obj);
			}
			return Task.CompletedTask;
		}

		public async Task<List<Stock>> GetAllAsync()
		{			
			return await _context.Set<Stock>().AsNoTracking().ToListAsync();
		}

		public async Task<Stock> GetByIdAsync(int id)
		{			
			return await _context.Set<Stock>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Stock?> GetStockByToyIdAsync(int toyId)
		{								
			return await _context.Set<Stock>().AsNoTracking().FirstOrDefaultAsync(x => x.ToyId == toyId);								
		}

		public Task UpdateAsync(Stock obj)
		{
			_context.Set<Stock>().Update(obj);

			return Task.CompletedTask;
		}
	}
}
