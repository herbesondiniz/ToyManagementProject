using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IRepositoryBase<OrderItem> _repository;
        public OrderItemService(IRepositoryBase<OrderItem> repositoryBase)
        {
			_repository = repositoryBase;
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
			return await _repository.GetByIdAsync(id);
		}
		public async Task UpdateAsync(OrderItem obj)
		{
			await _repository.UpdateAsync(obj);
		}
	}
}
