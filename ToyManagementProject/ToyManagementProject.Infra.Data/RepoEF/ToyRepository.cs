using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class ToyRepository : IToyRepository
	{
		private readonly IRepositoryBase<Toy> _repository;

        public ToyRepository(IRepositoryBase<Toy> repositoryBase)
        {
			_repository = repositoryBase;
		}

        public async Task AddAsync(Toy toy)
		{
			await _repository.AddAsync(toy);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);
		}

		public async Task<List<Toy>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Toy> GetByIdAsync(int id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Toy toy)
		{
			await _repository.UpdateAsync(toy);
		}
	}
}
