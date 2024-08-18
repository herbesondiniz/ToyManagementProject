using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;
		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		[HttpGet]
		public async Task<ActionResult<List<Toy>>> GetAll()
		{
			var toys = await _orderService.GetAllAsync();
			return Ok(toys);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Toy>> GetById(int id)
		{
			var toy = await _orderService.GetByIdAsync(id);
			if (toy == null)
			{
				return NotFound();
			}
			return Ok(toy);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Order order)
		{
			await _orderService.AddAsync(order);
			return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Order order)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			await _orderService.UpdateAsync(order);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var order = await _orderService.GetByIdAsync(id);
			if (order == null)
			{
				return NotFound();
			}

			await _orderService.DeleteAsync(id);
			return NoContent();
		}
	}
}
