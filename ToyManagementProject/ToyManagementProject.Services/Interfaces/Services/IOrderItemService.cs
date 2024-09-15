using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos.OrderItem;

namespace ToyManagementProject.Domain.Interfaces.Services
{
    public interface IOrderItemService
	{
		Task<Result<List<OrderItemDto>>> GetAllAsync();
		Task<OrderItem> GetByIdAsync(int id);
		Task AddAsync(OrderItem obj);
		Task UpdateAsync(OrderItem obj);
		Task DeleteAsync(int id);
	}
}
