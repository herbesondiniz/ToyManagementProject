using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace StockManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class StockController : ControllerBase
	{
		private readonly IStockService _StockService;
		private readonly IToyService _toyService;

		public StockController(IStockService StockService, IToyService toyService)
		{
			_StockService = StockService;
			_toyService = toyService;
		}
		[HttpGet]
		public async Task<ActionResult<List<Stock>>> GetAll()
		{
			var stocks = await _StockService.GetAllAsync();
			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Stock>> GetById(int id)
		{
			var stock = await _StockService.GetByIdAsync(id);
			if (stock == null)
			{
				return NotFound();
			}
			return Ok(stock);
		}

		[HttpPost]
		public async Task<ActionResult> Create(Stock stock)
		{
			var toy = _toyService.GetByIdAsync(stock.ToyId);
			
			if(toy == null)
				return NotFound("This toy is not registered. Please, you need to register it before.");

			if (stock.Quantity <= 0)
				return NotFound("Quantity is not filled");

			await _StockService.AddAsync(stock);
			
			return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Stock stock)
		{
			if (id != stock.Id)
			{
				return BadRequest();
			}

			var toy = _toyService.GetByIdAsync(stock.ToyId);

			if (toy == null)
				return NotFound("This toy is not registered.");

			await _StockService.UpdateAsync(stock);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var stock = await _StockService.GetByIdAsync(id);
			if (stock == null)
			{
				return NotFound();
			}

			await _StockService.DeleteAsync(id);
			return NoContent();
		}
	}
}
