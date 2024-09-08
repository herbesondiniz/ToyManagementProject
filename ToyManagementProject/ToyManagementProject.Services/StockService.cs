
using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;

namespace ToyManagementProject.Services
{
	public class StockService : IStockService
	{
		private readonly IRepositoryBase<Stock> _repository;
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
			_repository = repositoryBase;
			_stockRepository = stockRepository;
			_uow = uow;
			_toyService = toyService;
			_mapper = mapper;
		}
		public async Task<Result<Stock>> AddAsync(Stock stock)
		{
			var result = await _toyService.GetByIdAsync(stock.ToyId);
			
			if (!result.IsSuccess) 
			{
				return Result<Stock>.Failure(result.Errors);
			}		

			try
			{
				if (!stock.IsValid())
				{
					return Result<Stock>.Failure(stock.ErrorsNotifications);
				}

				await _repository.AddAsync(stock);

				await _uow.CommitAsync();
				
				stock.SetToy(_mapper.Map<Toy>(result.Data));

				return Result<Stock>.Success(stock);
			}
			catch (Exception ex)
			{
				return Result<Stock>.Failure($"Error AddAsync: {ex.Message}");
			}
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);	
		}
		public async Task<List<Stock>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Stock> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Stock> GetStockByToyIdAsync(int toyId)
		{
			return await _stockRepository.GetStockByToyIdAsync(toyId);
		}

		public async Task UpdateAsync(Stock obj)
		{
			await (_repository.UpdateAsync(obj));	
		}
	}
}
