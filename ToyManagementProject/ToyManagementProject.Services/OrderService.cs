using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepositoryBase<Order> _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;
        public OrderService(IRepositoryBase<Order> orderRepository, IOrderItemRepository orderItemRepository)
        {
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
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
			var orders = await _orderRepository.GetAllAsync();

			foreach (var order in orders) 
			{
				var items = await _orderItemRepository.GetAllAsync();
				
				items = items.Where(x => x.OrderId == order.Id).ToList();
				
				order.Items = items;
			}

			return orders;
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			var items = await _orderItemRepository.GetAllAsync();
			
			var order = await (_orderRepository.GetByIdAsync(id));

			items = items.Where(x => x.OrderId == id).ToList();			
			
			order.Items = items;

			return order;
		}

		public async Task UpdateAsync(Order obj)
		{
			await (_orderRepository.UpdateAsync(obj));
		}
	}
}
