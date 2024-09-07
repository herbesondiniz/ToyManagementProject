using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IServiceBase<OrderItem> _serviceBase;
        public OrderItemService(IServiceBase<OrderItem> serviceBase)
        {
			_serviceBase = serviceBase;
		}
        public async Task AddAsync(OrderItem obj)
		{
			await _serviceBase.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _serviceBase.DeleteAsync(id);
		}

		public async Task<List<OrderItem>> GetAllAsync()
		{
			return await _serviceBase.GetAllAsync();
		}
		public async Task<OrderItem> GetByIdAsync(int id)
		{
			return await _serviceBase.GetByIdAsync(id);
		}
		public async Task UpdateAsync(OrderItem obj)
		{
			await _serviceBase.UpdateAsync(obj);
		}
	}
}
