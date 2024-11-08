using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class OrderRepository : IOrderRepository
	{		
		private readonly ToyDbContext _context;
		public OrderRepository(ToyDbContext context)
        {
			_context = context;
		}
        public async Task AddAsync(Order obj)
		{
			await _context.Set<Order>().AddAsync(obj);
		}

		public Task DeleteAsync(int id)
		{
			var obj = GetByIdAsync(id).Result;
			if (obj != null)
			{
				_context.Set<Order>().Remove(obj);
			}

			return Task.CompletedTask;
		}

		public async Task<List<Order>> GetAllAsync(Func<IQueryable<Order>, IQueryable<Order>> include = null)
		{
			IQueryable<Order> query = _context.Order;

			if (include != null)
			{
				query = include(query);
			}

			return await query.AsNoTracking().ToListAsync();			
		}

		public async Task<Order?> GetByIdAsync(int id, Func<IQueryable<Order>, IQueryable<Order>> include = null)
		{
			IQueryable<Order> query = _context.Order;

			if (include != null)
			{
				query = include(query);
			}

			return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);						
		}

		public Task UpdateAsync(Order obj)
		{
			_context.Set<Order>().Update(obj);

			return Task.CompletedTask;
		}
	}
}
