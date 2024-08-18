using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;

namespace ToyManagementProject.Infra.Data.RepoEF
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IRepositoryBase<Order> _repository;
        public OrderRepository(IRepositoryBase<Order> _repositoryBase)
        {
			_repository = _repositoryBase;

		}
        public async Task AddAsync(Order obj)
		{
			await _repository.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _repository.DeleteAsync(id);
		}

		public async Task<List<Order>> GetAllAsync()
		{
			return await _repository.GetAllAsync();
		}

		public Task<Order> GetByIdAsync(int id)
		{
			return _repository.GetByIdAsync(id);
		}

		public async Task UpdateAsync(Order obj)
		{
			await _repository.UpdateAsync(obj);	
		}
	}
}
