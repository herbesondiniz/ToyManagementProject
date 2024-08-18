using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IRepositoryBase<TEntity> where TEntity: class
	{
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(int id);
		Task AddAsync(TEntity obj);
		Task UpdateAsync(TEntity obj);
		Task DeleteAsync(int id);
	}
}
