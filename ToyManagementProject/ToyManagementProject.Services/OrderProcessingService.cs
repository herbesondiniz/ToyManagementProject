using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class OrderProcessingService : IOrderProcessingService
	{
		private readonly IToyService _toyService;
		private readonly IStockService _stockService;
		private readonly IMapper _mapper;
		public OrderProcessingService(IToyService toyService, IStockService stockService, IMapper mapper)
		{
			_toyService = toyService;
			_stockService = stockService;
			_mapper = mapper;
		}
		public async Task<Result<object>> ProcessOrderAsync(Order order)
		{
			try
			{
				foreach (var orderItem in order.Items)
				{
					orderItem.SetOrderId(order.Id);

					var result = await _toyService.GetByIdAsync(orderItem.ToyId);					
					if (!result.IsSuccess)
					{
						return Result<object>.Failure($"{result.Errors}");
					}
					
					orderItem.SetToy(_mapper.Map<Toy>(result.Data));										

					var stock = await _stockService.GetStockByToyIdAsync(orderItem.Toy.Id);
																
					stock.DeductFromStock(orderItem.Quantity);
					
					if (stock.ErrorsNotifications.Any()) 
					{
						return Result<object>.Failure($"{stock.ErrorsNotifications}");
					}									

					if (!orderItem.IsValid()) 
					{
						return Result<object>.Failure($"{orderItem.ErrorsNotifications}");
					}					
				}
				if (order.TotalAmount <= 0)
				{
					return Result<object>.Failure($"Orders list is empty");
				}

				return Result<object>.Success(null, "Order created");
			}
			catch (Exception ex)
			{
				return Result<object>.Failure($"An error occurred while processing the order: {ex.Message} ");
			}
		}
	}
}
