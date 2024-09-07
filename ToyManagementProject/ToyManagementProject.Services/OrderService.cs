using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using AutoMapper;
using ToyManagementProject.Services.Validators.Interfaces;
using ToyManagementProject.Domain.DTOs;

namespace ToyManagementProject.Services
{
	public class OrderService : IOrderService
	{		
		private readonly IServiceBase<Order> _serviceBase;
		private readonly IOrderItemService _orderItemService;
		private readonly IOrderProcessingService _orderProcessingService;
		private readonly IValidator<Order> _orderValidator;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public OrderService(IServiceBase<Order> serviceBase,
							IOrderItemService orderItemService,
							IOrderProcessingService orderProcessingService,
							IValidator<Order> orderValidator,
							IUnitOfWork uow,							
							IMapper mapper							
							)
		{
			_serviceBase = serviceBase;
			_orderItemService = orderItemService;
			_orderProcessingService = orderProcessingService;
			_orderValidator = orderValidator;
			_uow = uow;			
			_mapper = mapper;			
		}
		public async Task<Result<OrderDTO>> AddAsync(OrderDTO orderDTO)
		{		
			var orderItems = orderDTO.Items
				.Select(item => new OrderItem(item.ToyId, item.Quantity))
				.ToList();			

			var order = new Order(orderDTO.ClientId, orderItems);
			
			//validação pela prória classe através de construtor
			if (order.ErrorsNotifications.Any())
				return Result<OrderDTO>.Failure(order.ErrorsNotifications);
			
			//validação por classe externa
			//var validateErrors = _orderValidator.Validate(order);

			//if (validateErrors.Any())
			//	return Result<OrderDTO>.Failure(validateErrors);

			var processResult = await _orderProcessingService.ProcessOrderAsync(order);

			if (!processResult.IsSuccess)
				return Result<OrderDTO>.Failure(processResult.Errors);

			try
			{
				await _serviceBase.AddAsync(order);
				
				await _uow.CommitAsync();
				
				orderDTO = _mapper.Map<OrderDTO>(order);

				return Result<OrderDTO>.Success(orderDTO, "Order created successfully");
			}
			catch (Exception ex)
			{
				await _uow.RollbackAsync();
				return Result<OrderDTO>.Failure(new List<string> { $"An error occurred while processing the order: {ex.Message} " });
			}			
		}	

		public async Task DeleteAsync(int id)
		{
			await _serviceBase.DeleteAsync(id);
		}

		public async Task<Result<IEnumerable<OrderDTO>>> GetAllAsync()
		{
			try
			{
				var orders = await _serviceBase.GetAllAsync();

				if (orders == null || orders.Count == 0)
				{
					return Result<IEnumerable<OrderDTO>>.Success(Enumerable.Empty<OrderDTO>(), "Orders list is empty");
				}

				var allItems = await _orderItemService.GetAllAsync();
				var itemsByOrderId = allItems.GroupBy(x => x.OrderId);

				foreach (var order in orders)
				{										
					var items = itemsByOrderId.FirstOrDefault(g => g.Key == order.Id)?.ToList() ?? new List<OrderItem>();

					order.AddItems(items);
				}

				var ordersDTO = _mapper.Map<IEnumerable<OrderDTO>>(orders);

				return Result<IEnumerable<OrderDTO>>.Success(ordersDTO);
			}
			catch (Exception ex)
			{
				return Result<IEnumerable<OrderDTO>>.Failure(new List<string> { $"Error GetAllAsync: {ex.Message} " });
			}			
		}

		public async Task<Order> GetByIdAsync(int id)
		{
			var items = await _orderItemService.GetAllAsync();

			var order = await (_serviceBase.GetByIdAsync(id));

			items = items.Where(x => x.OrderId == id).ToList();

			order.AddItems(items);

			return order;
		}

		public async Task UpdateAsync(Order obj)
		{
			await (_serviceBase.UpdateAsync(obj));
		}	
	}
}
