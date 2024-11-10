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
		public async Task<Result<object>> ProcessOrderStockAsync(Order order)
		{			
			try
			{
				var tasks = order.Items.Select(async orderItem =>
				{
					orderItem.SetOrderId(order.Id);

					var toy = await FetchAndValidateToy(orderItem.ToyId);
					if (toy == null) return Result<object>.Failure("Toy validation failed.");

					orderItem.SetToy(toy);

					var stock = await FetchAndValidateStock(orderItem.ToyId);
					if (stock == null) return Result<object>.Failure("Stock validation failed.");
					
					stock.UpdateStock(orderItem.Quantity);
					
					if(!stock.IsValid()) return Result<object>.Failure(stock.ErrorsNotifications);

					await _stockService.UpdateAsync(stock);

					return Result<object>.Success("");
				}).ToList();

				var results = await Task.WhenAll(tasks);
				if (results.Any(result => !result.IsSuccess))
				{
					var message = results.ToList().Where(x => !x.IsSuccess).FirstOrDefault().Errors;
					return Result<object>.Failure(message);
				}				

				return Result<object>.Success("");
			}
			catch (Exception ex)
			{
				return Result<object>.Failure($"An error occurred while processing the order: {ex.Message} ");
			}
		}
		private async Task<Toy> FetchAndValidateToy(int toyId)
		{
			var result = await _toyService.GetByIdAsync(toyId);
			if (!result.IsSuccess) return null;

			var toy = _mapper.Map<Toy>(result.Data);
			return toy.IsValid() ? toy : null;
		}

		private async Task<Stock> FetchAndValidateStock(int toyId)
		{
			var result = await _stockService.GetStockByToyIdAsync(toyId);
			if (!result.IsSuccess) return null;

			var stock = _mapper.Map<Stock>(result.Data);
			return stock.IsValid() ? stock : null;
		}		
	}
}
