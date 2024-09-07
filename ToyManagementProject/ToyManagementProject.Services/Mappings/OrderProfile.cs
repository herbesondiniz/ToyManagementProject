using AutoMapper;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.Services.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderDto>();
			CreateMap<OrderDto, Order>();
			CreateMap<OrderItem, OrderItemDto>();
			CreateMap<OrderItemDto, OrderItem>();
		}
	}
}
