﻿using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Services;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		private readonly IToyService _toyService;
		private readonly IOrderItemService _orderItemService;
		private readonly IStockService _stockService;
		public OrderController(IOrderService orderService, 
							   IToyService toyService,
							   IOrderItemService orderItemService, 
							   IStockService stockService)
		{
			_orderService = orderService;
			_toyService = toyService;
			_orderItemService = orderItemService;
			_stockService = stockService;
		}
		[HttpGet]
		public async Task<ActionResult<List<Order>>> GetAll()
		{
			var orders = await _orderService.GetAllAsync();
			
			return Ok(orders);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Order>> GetById(int id)
		{
			var order = await _orderService.GetByIdAsync(id);

			if (order == null)			
				return NotFound("Order not found");
			
			return Ok(order);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Order order)
		{
			if (order.ClientId == 0)
				return NotFound("You need to pass a client");
						
			if (!order.IsValid(order))
				return NotFound("Your order is not all filled");

			order.Id = 0;
			await _orderService.AddAsync(order);
			
			foreach (var orderItem in order.Items) 
			{
				var toy = await _toyService.GetByIdAsync(orderItem.ToyId);				
				
				if (toy == null)
					return NotFound("You need to register this toy before create an order item");
				
				var stock = await _stockService.GetStockByToyIdAsync(orderItem.ToyId);
				
				if (stock.Quantity <= 0)
					return NotFound($"the Toy {stock.ToyId} don´t have stock.");				

				orderItem.Price = toy.Price;
				orderItem.OrderId = order.Id;
				orderItem.Id = 0; 
				
				await _orderItemService.AddAsync(orderItem);

				stock.Quantity = stock.Quantity - orderItem.Quantity;
			}

			if (order.TotalAmount == 0)
				return NotFound();

			return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
			
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Order order)
		{
			if (id != order.Id)			 
				return BadRequest();
		 
			if (!order.IsValid(order))
				return NotFound("Your order is not all filled");

			await _orderService.UpdateAsync(order);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var order = await _orderService.GetByIdAsync(id);
			
			if (order == null)			 
				return NotFound();			 

			await _orderService.DeleteAsync(id);
			
			return NoContent();
		}
	}
}
