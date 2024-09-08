
using AutoMapper;
using System.Collections.Generic;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos;

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
		public async Task<Result<StockDto>> AddAsync(Stock stock)
		{
			var result = await _toyService.GetByIdAsync(stock.ToyId);
			
			if (!result.IsSuccess) 
			{
				return Result<StockDto>.Failure(result.Errors);
			}		

			try
			{
				if (!stock.IsValid())
				{
					return Result<StockDto>.Failure(stock.ErrorsNotifications);
				}

				await _repository.AddAsync(stock);

				await _uow.CommitAsync();
				
				stock.SetToy(_mapper.Map<Toy>(result.Data));

				return Result<StockDto>.Success(_mapper.Map<StockDto>(stock));
			}
			catch (Exception ex)
			{
				return Result<StockDto>.Failure($"Error AddAsync: {ex.Message}");
			}
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);	
		}
		public async Task<Result<IList<StockDto>>> GetAllAsync()
		{
			var stocks = await _repository.GetAllAsync();
			
			if(stocks == null) 
			{
				return Result<IList<StockDto>>.Failure($"stocks list is empty");
			}

			return Result<IList<StockDto>>.Success(_mapper.Map<IList<StockDto>>(stocks));
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
