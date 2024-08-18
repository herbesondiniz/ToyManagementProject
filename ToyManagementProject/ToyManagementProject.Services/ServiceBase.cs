using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
	{
		private readonly IRepositoryBase<TEntity> _repositorybase;
		public ServiceBase(IRepositoryBase<TEntity> repositorybase)
		{
			_repositorybase = repositorybase;
		}
		public async Task AddAsync(TEntity obj)
		{
			await _repositorybase.AddAsync(obj);
		}
		public async Task DeleteAsync(int id)
		{
			await _repositorybase.DeleteAsync(id);
		}		
		public async Task<List<TEntity>> GetAllAsync()
		{
			return await _repositorybase.GetAllAsync();
		}	
		public async Task<TEntity> GetByIdAsync(int id)
		{
			return await _repositorybase.GetByIdAsync(id);
		}
		public async Task UpdateAsync(TEntity obj)
		{
			await _repositorybase.UpdateAsync(obj);
		}	
	}
}
