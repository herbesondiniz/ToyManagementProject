using AutoMapper;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Services.Dtos.Order;
using ToyManagementProject.Services.Dtos.OrderItem;

namespace ToyManagementProject.Services.Mappings
{
    public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, UpdateOrderDto>();
			CreateMap<UpdateOrderDto, Order>();

			CreateMap<OrderItem, OrderItemDto>();
			CreateMap<OrderItemDto, OrderItem>();

			CreateMap<Order, CreateOrderDto>();
			CreateMap<CreateOrderDto, Order>();

			CreateMap<Order, OrderDto>();
			CreateMap<OrderDto, Order>();
		}
	}
}
