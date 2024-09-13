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

					var resultToy = await _toyService.GetByIdAsync(orderItem.ToyId);					
					
					if (!resultToy.IsSuccess)
					{
						return Result<object>.Failure($"{resultToy.Errors}");
					}

					var toy = _mapper.Map<Toy>(resultToy.Data);

					if (!toy.IsValid()) 
					{
						return Result<object>.Failure($"{toy.ErrorsNotifications}");
					}

					orderItem.SetToy(toy);										

					var resultStock = await _stockService.GetStockByToyIdAsync(orderItem.Toy.Id);

					if (!resultStock.IsSuccess) 
					{
						return Result<object>.Failure($"{resultStock.Errors}");
					}

					var stock = _mapper.Map<Stock>(resultStock.Data);

					if (!stock.IsValid()) 
					{
						return Result<object>.Failure($"{stock.ErrorsNotifications}");
					}
																
					stock.DeductFromStock(orderItem.Quantity);																								
				}
				if (order.TotalAmount <= 0)
				{
					return Result<object>.Failure($"Error calculating the total amount.");
				}

				return Result<object>.Success("", "Order created");
			}
			catch (Exception ex)
			{
				return Result<object>.Failure($"An error occurred while processing the order: {ex.Message} ");
			}
		}
	}
}
