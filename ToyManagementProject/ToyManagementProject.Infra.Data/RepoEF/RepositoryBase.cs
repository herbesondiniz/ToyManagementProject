using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
	{
		private readonly ToyDbContext _context;

		public RepositoryBase(ToyDbContext context)
		{
			_context = context;
		}
		public async Task AddAsync(TEntity obj)
		{
			_context.Set<TEntity>().Add(obj);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(int id)
		{
			var obj = await _context.Set<TEntity>().FindAsync(id);
			if (obj != null)
			{
				_context.Set<TEntity>().Remove(obj);
				await _context.SaveChangesAsync();
			}
		}
		public async Task<List<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}

		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}
		public async Task UpdateAsync(TEntity obj)
		{
			_context.Set<TEntity>().Update(obj);
			await _context.SaveChangesAsync();
		}
	}
}