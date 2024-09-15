using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos.Stock;

namespace ToyManagementProject.Services
{
    public class StockService : IStockService
	{		
		private readonly IStockRepository _stockRepository;
		private readonly IToyService _toyService;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		public StockService(IRepositoryBase<Stock> repositoryBase,
							IStockRepository stockRepository, 
							IUnitOfWork uow,
							IToyService toyService,
							IMapper mapper)
        {			
			_stockRepository = stockRepository;
			_uow = uow;
			_toyService = toyService;
			_mapper = mapper;
		}		
		public async Task<Result<StockDto>> AddAsync(Stock stock)
		{
			if (!stock.IsValid())
			{
				return Result<StockDto>.Failure(stock.ErrorsNotifications);
			}

			var stockResult = await _toyService.GetByIdAsync(stock.ToyId);
			
			if (!stockResult.IsSuccess) 
			{
				return Result<StockDto>.Failure(stockResult.Errors);
			}
			
			try
			{				
				await _stockRepository.AddAsync(stock);

				await _uow.CommitAsync();
				
				stock.SetToy(_mapper.Map<Toy>(stockResult.Data));

				return Result<StockDto>.Success(_mapper.Map<StockDto>(stock));
			}
			catch (Exception ex)
			{
				return Result<StockDto>.Failure($"Error AddAsync: {ex.Message}");
			}
		}

		public async Task<Result<StockDto>> DeleteAsync(int id)
		{
			var stock = await _stockRepository.GetByIdAsync(id);

			if (stock == null)
			{
				return Result<StockDto>.Failure($"the stock is empty");
			}

			try
			{				
				await _stockRepository.DeleteAsync(id);

				await _uow.CommitAsync();

				var stockDto = _mapper.Map<StockDto>(stock);

				return Result<StockDto>.Success(stockDto);
			}
			catch (Exception ex)
			{
				return Result<StockDto>.Failure(new List<string> { $"Error Delete: {ex.Message}" });
			}
		}
		public async Task<Result<IList<StockDto>>> GetAllAsync()
		{
			var stocks = await _stockRepository.GetAllAsync();
			
			if(stocks == null || stocks.Count() == 0) 
			{
				return Result<IList<StockDto>>.Failure($"stocks list is empty");
			}

			return Result<IList<StockDto>>.Success(_mapper.Map<IList<StockDto>>(stocks));
		}
		public async Task<Result<StockDto>> GetByIdAsync(int id)
		{
			var stock = await _stockRepository.GetByIdAsync(id);
			
			if(stock == null) 
			{
				return Result<StockDto>.Failure($"Stock is empty");
			}
			
			return Result<StockDto>.Success(_mapper.Map<StockDto>(stock));
		}

		public async Task<Result<StockDto>> GetStockByToyIdAsync(int toyId)
		{
			var stock = await _stockRepository.GetStockByToyIdAsync(toyId);

			if(stock == null)
			{
				return Result<StockDto>.Failure($"the stock is empty");
			}

			return Result<StockDto>.Success(_mapper.Map<StockDto>(stock));
		}

		public async Task<Result<StockDto>> UpdateAsync(Stock stock)
		{
			if (!stock.IsValid()) 
			{
				return Result<StockDto>.Failure(stock.ErrorsNotifications);
			}

			var result = await _toyService.GetByIdAsync(stock.Id);

			if (!result.IsSuccess) 
			{
				return Result<StockDto>.Failure(result.Errors);
			}
				
			try
			{
				await _stockRepository.UpdateAsync(stock);
				
				await _uow.CommitAsync();
				
				return Result<StockDto>.Success(_mapper.Map<StockDto>(stock));
			}
			catch (Exception ex)
			{
				return Result<StockDto>.Failure(ex.Message);
			}				
		}		
	}
}
