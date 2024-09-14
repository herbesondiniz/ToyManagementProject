using Microsoft.EntityFrameworkCore;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Infra.Data.Context;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IRepositoryBase<Order> _repository;
		
		private readonly ToyDbContext _context;
		public OrderRepository(IRepositoryBase<Order> _repositoryBase, ToyDbContext context)
        {
			_repository = _repositoryBase;
			_context = context;
		}
        public async Task AddAsync(Order obj)
		{
			await _repository.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);
		}

		public async Task<List<Order>> GetAllAsync(Func<IQueryable<Order>, IQueryable<Order>> include = null)
		{
			IQueryable<Order> query = _context.Order;

			if (include != null)
			{
				query = include(query);
			}

			return await query.ToListAsync();
		}

		public async Task<Order> GetByIdAsync(int id, Func<IQueryable<Order>, IQueryable<Order>> include = null)
		{
			IQueryable<Order> query = _context.Order;

			if (include != null)
			{
				query = include(query);
			}

			return await query.Where(x => x.Id == id).FirstOrDefaultAsync();						
		}

		public async Task UpdateAsync(Order obj)
		{
			await _repository.UpdateAsync(obj);	
		}
	}
}
