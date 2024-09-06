using AutoMapper;
using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Services.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderDTO>();
			CreateMap<OrderDTO, Order>();
			CreateMap<OrderItem, OrderItemDTO>();
			CreateMap<OrderItemDTO, OrderItem>();
		}
	}
}
