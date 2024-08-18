using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderItemController : Controller
	{
		private readonly IOrderItemService _orderItemService;

		public OrderItemController(IOrderItemService orderItemService)
		{
			_orderItemService = orderItemService;
		}
		[HttpGet]
		public async Task<ActionResult<List<OrderItem>>> GetAll()
		{
			var orderItems = await _orderItemService.GetAllAsync();			
			
			return Ok(orderItems);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<OrderItem>> GetById(int id)
		{
			var orderItem = await _orderItemService.GetByIdAsync(id);
			if (orderItem == null)
			{
				return NotFound();
			}
			return Ok(orderItem);
		}

		[HttpPost]
		public async Task<ActionResult> Create(OrderItem orderItem)
		{
			await _orderItemService.AddAsync(orderItem);
			return CreatedAtAction(nameof(GetById), new { id = orderItem.ToyId }, orderItem);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, OrderItem orderItem)
		{
			if (id != orderItem.ToyId)
			{
				return BadRequest();
			}

			await _orderItemService.UpdateAsync(orderItem);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var toy = await _orderItemService.GetByIdAsync(id);
			if (toy == null)
			{
				return NotFound();
			}

			await _orderItemService.DeleteAsync(id);
			return NoContent();
		}
	}
}
