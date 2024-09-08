using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Services.Dtos;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Validators.Interfaces;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Services
{
	public class ToyService: IToyService
	{		
		private readonly IToyRepository _toyRepository;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<Toy> _toyValidator;
		public ToyService(IToyRepository toyRepository, IUnitOfWork uow, IMapper mapper, IValidator<Toy> toyValidator)
		{
			_toyRepository = toyRepository;
			_uow = uow;
			_mapper = mapper;
			_toyValidator = toyValidator;
		}
		public async Task<Result<ToyDto>> AddAsync(Toy toy)
		{
			try
			{
				if (!toy.IsValid()) 
				{
					return Result<ToyDto>.Failure(toy.ErrorsNotifications);
				}

				//var validateErrors = _toyValidator.Validate(toy);

				//if (validateErrors.Any())
				//	return Result<ToyDto>.Failure(validateErrors);
											
				await _toyRepository.AddAsync(toy);
				await _uow.CommitAsync();

				return Result<ToyDto>.Success(_mapper.Map<ToyDto>(toy));
			}
			catch (Exception ex)
			{
				return Result<ToyDto>.Failure(new List<string> { $"Error AddAsync: {ex.Message}" });
			}		
		}

		public async Task<Result<ToyDto>> DeleteAsync(int id)
		{
			try
			{
				var toy = _toyRepository.GetByIdAsync(id);
				
				if (toy == null) 
				{
					return Result<ToyDto>.Failure($"Toy doesn´t exists");
				}

				await _toyRepository.DeleteAsync(id);
				
				await _uow.CommitAsync();

				var toyDTO = _mapper.Map<ToyDto>(toy);

				return Result<ToyDto>.Success(toyDTO);
			}
			catch (Exception ex)
			{
				return Result<ToyDto>.Failure(new List<string> { $"Error Delete: {ex.Message}" });
			}
		}

		public async Task<Result<ToyDto>> GetByIdAsync(int id)
		{
			var toy = await _toyRepository.GetByIdAsync(id);
			if (toy == null)
			{
				return Result<ToyDto>.Failure($"Toy is not registered.");
			}

			return Result<ToyDto>.Success(_mapper.Map<ToyDto>(toy));
		}	
		public async Task<Result<ToyDto>> UpdateAsync(Toy toy)
		{						
			if (toy.ErrorsNotifications.Any())
            {
				return Result<ToyDto>.Failure(toy.ErrorsNotifications);
			}

            try
			{				
				await _toyRepository.UpdateAsync(toy);
				await _uow.CommitAsync();

				return Result<ToyDto>.Success(_mapper.Map<ToyDto>(toy));
			}
			catch (Exception ex)
			{
				return Result<ToyDto>.Failure(new List<string> { $"Error UpdateAsync: {ex.Message}" });
				throw;
			}
			
		}

		public async Task<Result<IEnumerable<ToyDto>>> GetAllAsync()
		{
			var toys = await _toyRepository.GetAllAsync();
			if (toys == null)
			{
				return Result<IEnumerable<ToyDto>>.Failure($"toys is empty");
			}

			return Result<IEnumerable<ToyDto>>.Success(_mapper.Map<IEnumerable<ToyDto>>(toys));


		}
	}
}
