using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos;

namespace StockManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class StockController : ControllerBase
	{
		private readonly IStockService _stockService;
		private readonly IToyService _toyService;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public StockController(IStockService StockService, IToyService toyService, IUnitOfWork uow, IMapper mapper)
		{
			_stockService = StockService;
			_toyService = toyService;
			_uow = uow;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<List<Stock>>> GetAll()
		{
			var result = await _stockService.GetAllAsync();
			
			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return Ok(result);
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
		public async Task<ActionResult> Create(StockDto dto)
		{
			var result = await _stockService.AddAsync(_mapper.Map<Stock>(dto));

			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = dto.Id }, result);
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
			var stock = await _stockService.GetByIdAsync(id);
			if (stock == null)
			{
				return NotFound();
			}

			try
			{
				await _stockService.DeleteAsync(id);
				
				await _uow.CommitAsync();
			}
			catch (Exception)
			{
				await _uow.RollbackAsync();			
			}
			
			return NoContent();
		}
	}
}
