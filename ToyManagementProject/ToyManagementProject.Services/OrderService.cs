using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;

namespace ToyManagementProject.Services
{
	public class OrderService : IOrderService
	{
		private readonly IRepositoryBase<Order> _orderRepository;
		private readonly IOrderItemRepository _orderItemRepository;

		private readonly IToyService _toyService;
		private readonly IOrderItemService _orderItemService;
		private readonly IStockService _stockService;
		private readonly IUnitOfWork _uow;
		public OrderService(IRepositoryBase<Order> orderRepository, 
						    IOrderItemRepository orderItemRepository, 
						    IToyService toyService,
							IOrderItemService orderItemService,
							IStockService stockService,
							IUnitOfWork uow
							)
        {
			_orderRepository = orderRepository;
			_orderItemRepository = orderItemRepository;
			_toyService = toyService;
			_orderItemService = orderItemService;
			_stockService = stockService;
			_uow = uow;
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

		public async Task<bool> ProcessOrderAsync(Order order)
		{
			if (!order.IsValid(order)) return false;

			order.Id = 0;			
			await AddAsync(order);  // método existente no serviço para adicionar o pedido

			foreach (var orderItem in order.Items)
			{				
				var toy = await _toyService.GetByIdAsync(orderItem.ToyId);
				if (toy == null) return false;

				var stock = await _stockService.GetStockByToyIdAsync(orderItem.ToyId);
				if (stock.Quantity <= 0) return false;

				orderItem.Price = toy.Price;
				orderItem.OrderId = order.Id;
				stock.Quantity -= orderItem.Quantity;																
			}
			
			return true;
		}
	}
}
