using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IToyService
	{
		Task<List<Toy>> GetAllAsync();
		Task<Toy> GetByIdAsync(int id);
		Task<Result<ToyDTO>> AddAsync(ToyDTO toyDTO);
		Task<Result<ToyDTO>> UpdateAsync(ToyDTO toyDTO);
		Task DeleteAsync(int id);
	}
}
