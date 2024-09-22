using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos.Order;

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
		private readonly IMapper _mapper;
		public OrderController(IOrderService orderService,
							   IToyService toyService,
							   IOrderItemService orderItemService,
							   IStockService stockService,
							   IUnitOfWork uow,
							   IMapper mapper)
		{
			_orderService = orderService;
			_toyService = toyService;
			_orderItemService = orderItemService;
			_stockService = stockService;
			_uow = uow;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<List<OrderDto>>> GetAll()
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
		public async Task<ActionResult> Create(CreateOrderDto createOrderDto)
		{
			var order = _mapper.Map<Order>(createOrderDto);
			
			var result = await _orderService.AddAsync(order);

			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, UpdateOrderDto updateOrderDto)
		{
			if (id != updateOrderDto.Id)
			{
				return BadRequest();
			}
			var result = await _orderService.UpdateAsync(_mapper.Map<Order>(updateOrderDto));

			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _orderService.DeleteAsync(id);

			if (!result.IsSuccess)
				return UnprocessableEntity(result.Errors);

			return Ok(result.Data);
		}
	}
}
