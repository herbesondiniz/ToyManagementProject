﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyManagementProject.Services.Dtos.OrderItem;

namespace ToyManagementProject.Services.Dtos.Order
{
	public class OrderDto
	{
		public int Id { get; set; }
		public int ClientId { get; set; }
		public List<OrderItemDto>? Items { get; set; }
		public decimal TotalAmount { get; set; }
	}
}
