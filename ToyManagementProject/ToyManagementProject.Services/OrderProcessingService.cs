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
		public OrderProcessingService(IToyService toyService, IStockService stockService, IMapper mapper)
		{
			_toyService = toyService;
			_stockService = stockService;
		}
		public async Task<Result<object>> ProcessOrderAsync(Order order)
		{
			try
			{
				foreach (var orderItem in order.Items)
				{
					var result = await _toyService.GetByIdAsync(orderItem.ToyId);
					if (!result.IsSuccess)
					{
						return Result<object>.Failure($"Toy doesn´t exists");
					}
					var toy = result.Data;

					var stock = await _stockService.GetStockByToyIdAsync(orderItem.ToyId);
					if (stock?.Quantity <= 0)
						return Result<object>.Failure($"Orders list is empty");

					orderItem.AddPrice(toy.Price);
					orderItem.AddOrderId(order.Id);
					stock.Quantity -= orderItem.Quantity;
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
