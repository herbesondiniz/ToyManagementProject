using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Interfaces.Services
{
	public interface IOrderService 
	{	
		Task<Result<OrderDTO>> AddAsync(OrderDTO orderDTO);
		Task<Result<IEnumerable<OrderDTO>>> GetAllAsync();
		Task<Order> GetByIdAsync(int id);		
		Task UpdateAsync(Order obj);
		Task DeleteAsync(int id);
	}
}
