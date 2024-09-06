using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Domain.DTOs;

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
		private readonly IUnitOfWork _uow;
		public OrderController(IOrderService orderService,
							   IToyService toyService,
							   IOrderItemService orderItemService,
							   IStockService stockService,
							   IUnitOfWork uow)
		{
			_orderService = orderService;
			_toyService = toyService;
			_orderItemService = orderItemService;
			_stockService = stockService;
			_uow = uow;
		}
		[HttpGet]
		public async Task<ActionResult<List<OrderDTO>>> GetAll()
		{
			var ordersDTO = await _orderService.GetAllAsync();

			return Ok(ordersDTO);
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
		public async Task<ActionResult> Create(OrderDTO orderDTO)
		{
			var result = await _orderService.AddAsync(orderDTO);

			if (!result.IsSuccess)
				return UnprocessableEntity(result.Errors);			

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);

		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Order order)
		{
			if (id != order.Id)
				return BadRequest();

			//if (order.Notifications.Any())
			//	return NotFound("Your order is not all filled");

			try
			{
				await _orderService.UpdateAsync(order);

				await _uow.CommitAsync();
			}
			catch (Exception)
			{
				await _uow.RollbackAsync();
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var order = await _orderService.GetByIdAsync(id);

			if (order == null)
				return NotFound();
			try
			{
				await _orderService.DeleteAsync(id);
			}
			catch (Exception)
			{

				await _uow.RollbackAsync();
			}

			await _uow.CommitAsync();

			return NoContent();
		}
	}
}
