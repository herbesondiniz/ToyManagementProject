using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos.Toy;

namespace ToyManagementProject.Domain.Interfaces.Services
{
    public interface IToyService
	{
		Task<Result<IEnumerable<ToyDto>>> GetAllAsync();
		Task<Result<ToyDto>> GetByIdAsync(int id);
		Task<Result<ToyDto>> AddAsync(Toy toy);
		Task<Result<ToyDto>> UpdateAsync(Toy toy);
		Task<Result<ToyDto>> DeleteAsync(int id);
	}
}
