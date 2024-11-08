using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class ToyRepository : IToyRepository
	{		
		private readonly ToyDbContext _context;
		
		public ToyRepository(ToyDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(Toy toy)
		{
			await _context.Set<Toy>().AddAsync(toy);
		}

		public Task DeleteAsync(int id)
		{
			var obj = GetByIdAsync(id).Result;
			if (obj != null)
			{
				_context.Set<Toy>().Remove(obj);
			}
			return Task.CompletedTask;
		}

		public async Task<List<Toy>> GetAllAsync()
		{			
			return await _context.Set<Toy>().AsNoTracking().ToListAsync();
		}

		public async Task<Toy> GetByIdAsync(int id)
		{
			return await _context.Set<Toy>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task UpdateAsync(Toy toy)
		{
			_context.Set<Toy>().Update(toy);

			return Task.CompletedTask;
		}		
	}
}
