﻿using ToyManagementProject.Services.Dtos.OrderItem;

namespace ToyManagementProject.Services.Dtos.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public List<OrderItemDto>? Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}