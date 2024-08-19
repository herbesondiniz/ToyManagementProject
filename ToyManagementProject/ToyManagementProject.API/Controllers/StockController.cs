using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;

namespace StockManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class StockController : ControllerBase
	{
		private readonly IStockService _stockService;
		private readonly IToyService _toyService;
		private readonly IUnitOfWork _uow;

		public StockController(IStockService StockService, IToyService toyService, IUnitOfWork uow)
		{
			_stockService = StockService;
			_toyService = toyService;
			_uow = uow;
		}
		[HttpGet]
		public async Task<ActionResult<List<Stock>>> GetAll()
		{
			var stocks = await _stockService.GetAllAsync();
			return Ok(stocks);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Stock>> GetById(int id)
		{
			var stock = await _stockService.GetByIdAsync(id);
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
			try
			{
				await _stockService.AddAsync(stock);
				
				await _uow.Commit();
			}
			catch (Exception)
			{
				await _uow.Rollback();
			}
			
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

			try
			{
				await _stockService.UpdateAsync(stock);

				await _uow.Commit();
			}
			catch (Exception)
			{
				await _uow.Rollback();
			}
		
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var stock = await _stockService.GetByIdAsync(id);
			if (stock == null)
			{
				return NotFound();
			}

			try
			{
				await _stockService.DeleteAsync(id);
				
				await _uow.Commit();
			}
			catch (Exception)
			{
				await _uow.Rollback();			
			}
			
			return NoContent();
		}
	}
}
