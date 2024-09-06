using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Repositories
{
	public interface IToyRepository
	{
		Task<List<Toy>> GetAllAsync();
		Task<Toy> GetByIdAsync(int id);
		Task AddAsync(Toy toy);
		Task UpdateAsync(Toy toy);
		Task DeleteAsync(int id);
	}
}
