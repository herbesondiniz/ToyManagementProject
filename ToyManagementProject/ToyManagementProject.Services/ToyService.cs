using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.DTOs;
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
		public async Task<Result<ToyDTO>> AddAsync(ToyDTO toyDTO)
		{
			var toy = new Toy(toyDTO.Name, toyDTO.Description, toyDTO.Price);

			try
			{
				if (toy.ErrorsNotifications.Any()) 
				{
					return Result<ToyDTO>.Failure(toy.ErrorsNotifications);
				}

				var validateErrors = _toyValidator.Validate(toy);

				if (validateErrors.Any())
					return Result<ToyDTO>.Failure(validateErrors);
											
				await _serviceBase.AddAsync(toy);
				await _uow.CommitAsync();

				toyDTO = _mapper.Map<ToyDTO>(toy);

				return Result<ToyDTO>.Success(toyDTO);
			}
			catch (Exception ex)
			{
				return Result<ToyDTO>.Failure(new List<string> { $"Error AddAsync: {ex.Message}" });
			}		
		}

		public async Task DeleteAsync(int id)
		{
			await _serviceBase.DeleteAsync(id);
		}
	
		public async Task<Toy> GetByIdAsync(int id)
		{
			return await _serviceBase.GetByIdAsync(id);
		}

		public async Task<Result<ToyDTO>> UpdateAsync(ToyDTO toyDTO)
		{
			var toy = new Toy("","",0);
			
			toy.UpdateToy(toyDTO.Id, toyDTO.Name, toyDTO.Description, toyDTO.Price);

			if (toy.ErrorsNotifications.Any())
            {
				return Result<ToyDTO>.Failure(toy.ErrorsNotifications);
			}

            try
			{				
				await _serviceBase.UpdateAsync(toy);
				await _uow.CommitAsync();

				toyDTO = _mapper.Map<ToyDTO>(toy);

				return Result<ToyDTO>.Success(toyDTO);
			}
			catch (Exception ex)
			{
				return Result<ToyDTO>.Failure(new List<string> { $"Error UpdateAsync: {ex.Message}" });
				throw;
			}
			
		}

		public async Task<List<Toy>> GetAllAsync()
		{
			return await _serviceBase.GetAllAsync();

		}		
	}
}
