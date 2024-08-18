using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class OrderItemRepository : IOrderItemRepository
	{
		private readonly IRepositoryBase<OrderItem> _repository;
        public OrderItemRepository(IRepositoryBase<OrderItem> repository)
        {
			_repository = repository;

		}
        public async Task AddAsync(OrderItem obj)
		{
			await _repository.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);	
		}

		public async Task<List<OrderItem>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<OrderItem> GetByIdAsync(int id)
		{
			return await (_repository.GetByIdAsync(id));	
		}

		public async Task UpdateAsync(OrderItem obj)
		{
			await(_repository.UpdateAsync(obj));	
		}
	}
}
