using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IOrderService 
	{	
		Task<Result<OrderDto>> AddAsync(Order order);
		Task<Result<IEnumerable<OrderDto>>> GetAllAsync();
		Task<Result<OrderDto>> GetByIdAsync(int id);		
		Task<Result<OrderDto>> UpdateAsync(Order order);
		Task<Result<OrderDto>> DeleteAsync(int id);
	}
}
