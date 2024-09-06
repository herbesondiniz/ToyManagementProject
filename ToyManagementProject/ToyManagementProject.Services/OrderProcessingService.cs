using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.DTOs;
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
					var toy = await _toyService.GetByIdAsync(orderItem.ToyId);
					if (toy == null)
						return Result<object>.Failure(new List<string> { $"Orders list is empty" });										

					var stock = await _stockService.GetStockByToyIdAsync(orderItem.ToyId);
					if (stock?.Quantity <= 0)
						return Result<object>.Failure(new List<string> { $"Stock is not registered" });					

					orderItem.Price = toy.Price;
					orderItem.OrderId = order.Id;
					stock.Quantity -= orderItem.Quantity;
				}
				if (order.TotalAmount <= 0)
				{
					return Result<object>.Failure(new List<string> { $"Total amount can not be zero" });					
				}				

				return Result<object>.Success(null, "Order created");
			}
			catch (Exception ex)
			{
				return Result<object>.Failure(new List<string> { $"An error occurred while processing the order: {ex.Message} " });
			}					
		}
	}
}
