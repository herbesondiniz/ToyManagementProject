using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;

namespace ToyManagementProject.Domain.Mappings
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
