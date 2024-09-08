using AutoMapper;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Repositories;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IOrderItemRepository _orderItemRepository;
		private readonly IMapper _mapper;
		public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
			_orderItemRepository = orderItemRepository;
			_mapper = mapper;
		}
        public async Task AddAsync(OrderItem obj)
		{
			await _orderItemRepository.AddAsync(obj);
		}				
		public async Task<Result<List<OrderItemDto>>> GetAllAsync()
		{
			var orderItems = await _orderItemRepository.GetAllAsync();
			if (orderItems == null) 
			{
				return Result<List<OrderItemDto>>.Failure($"Items of orders is empty ");
			}
			return Result<List<OrderItemDto>>.Success(_mapper.Map<List<OrderItemDto>>(orderItems));
		}
		public async Task<OrderItem> GetByIdAsync(int id)
		{
			return await _orderItemRepository.GetByIdAsync(id);
		}
		public async Task UpdateAsync(OrderItem obj)
		{
			await _orderItemRepository.UpdateAsync(obj);
		}
		public async Task DeleteAsync(int id)
		{
			await _orderItemRepository.DeleteAsync(id);
		}
	}
}
