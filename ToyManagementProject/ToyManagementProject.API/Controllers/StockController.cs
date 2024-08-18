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

		public StockController(IStockService StockService)
		{
			_StockService = StockService;
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
