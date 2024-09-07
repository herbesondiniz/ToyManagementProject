
using ToyManagementProject.Domain.DTOs;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IServiceBase<TEntity> where TEntity : class
	{
		Task<List<TEntity>> GetAllAsync();
		Task<TEntity> GetByIdAsync(int id);
		Task AddAsync(TEntity obj);
		Task UpdateAsync(TEntity obj);
		Task DeleteAsync(int id);
	}
}
