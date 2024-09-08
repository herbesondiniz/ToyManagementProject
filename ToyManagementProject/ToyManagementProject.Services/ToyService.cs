using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Services.Dtos;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Validators.Interfaces;

namespace ToyManagementProject.Services
{
	public class ToyService: IToyService
	{		
		private readonly IServiceBase<Toy> _serviceBase;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<Toy> _toyValidator;
		public ToyService(IServiceBase<Toy> serviceBase, IUnitOfWork uow, IMapper mapper, IValidator<Toy> toyValidator)
		{
			_serviceBase = serviceBase;
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
											
				await _serviceBase.AddAsync(toy);
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
				var toy = _serviceBase.GetByIdAsync(id);
				
				if (toy == null) 
				{
					return Result<ToyDto>.Failure($"Toy doesn´t exists");
				}

				await _serviceBase.DeleteAsync(id);
				
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
			var toy = await _serviceBase.GetByIdAsync(id);
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
				await _serviceBase.UpdateAsync(toy);
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
			var toys = await _serviceBase.GetAllAsync();
			if (toys == null)
			{
				return Result<IEnumerable<ToyDto>>.Failure($"toys is empty");
			}

			return Result<IEnumerable<ToyDto>>.Success(_mapper.Map<IEnumerable<ToyDto>>(toys));


		}
	}
}
