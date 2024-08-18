using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepositoryBase<Order> _orderRepository;
        public OrderService(IRepositoryBase<Order> orderRepository)
        {
			_orderRepository = orderRepository;
		}
        public async Task AddAsync(Order obj)
		{
			await _orderRepository.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _orderRepository.DeleteAsync(id);	
		}

		public async Task<List<Order>> GetAllAsync()
		{
			return await _orderRepository.GetAllAsync();
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			return await (_orderRepository.GetByIdAsync(id));	
		}

		public async Task UpdateAsync(Order obj)
		{
			await (_orderRepository.UpdateAsync(obj));
		}
	}
}
