using AutoMapper;
using System.Collections.Generic;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Services
{
	public class OrderItemService : IOrderItemService
	{
		private readonly IServiceBase<OrderItem> _serviceBase;
		private readonly IMapper _mapper;
		public OrderItemService(IServiceBase<OrderItem> serviceBase, IMapper mapper)
        {
			_serviceBase = serviceBase;
			_mapper = mapper;
		}
        public async Task AddAsync(OrderItem obj)
		{
			await _serviceBase.AddAsync(obj);
		}

		public async Task DeleteAsync(int id)
		{
			await _serviceBase.DeleteAsync(id);
		}
		Task<Result<List<OrderItemDto>>> IOrderItemService.GetAllAsync()
		{
			throw new NotImplementedException();
		}
		public async Task<Result<List<OrderItemDto>>> GetAllAsync()
		{
			var orderItems = await _serviceBase.GetAllAsync();
			if (orderItems == null) 
			{
				return Result<List<OrderItemDto>>.Failure($"Items of orders is empty ");
			}
			return Result<List<OrderItemDto>>.Success(_mapper.Map<List<OrderItemDto>>(orderItems));
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
