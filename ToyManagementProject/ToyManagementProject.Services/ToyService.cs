using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class ToyService: IToyService
	{		
		private readonly IToyRepository _toyRepository;
		public ToyService(IToyRepository toyRepository)
		{		
			_toyRepository = toyRepository;
		}
		public async Task AddAsync(Toy toy)
		{
			await _toyRepository.AddAsync(toy);
		}

		public async Task DeleteAsync(int id)
		{
			await _toyRepository.DeleteAsync(id);
		}

		public async Task<List<Toy>> GetAllAsync()
		{
			return await _toyRepository.GetAllAsync();
		}

		public async Task<Toy> GetByIdAsync(int id)
		{
			return await _toyRepository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Toy toy)
		{
			await UpdateAsync(toy);
		}
	}
}
