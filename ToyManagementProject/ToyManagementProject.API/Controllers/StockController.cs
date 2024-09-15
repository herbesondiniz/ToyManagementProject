using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos.Stock;

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
		public async Task<ActionResult<List<StockDto>>> GetAll()
		{
			var result = await _stockService.GetAllAsync();

			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<StockDto>> GetById(int id)
		{
			var stockDto = await _stockService.GetByIdAsync(id);
			
			if (!stockDto.IsSuccess)
			{
				return NotFound();
			}

			return Ok(stockDto);
		}

		[HttpPost]
		public async Task<ActionResult> Create(CreateStockDto createStockdto)
		{
			var result = await _stockService.AddAsync(_mapper.Map<Stock>(createStockdto));

			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, UpdateStockDto updateStockDto)
		{
			if (id != updateStockDto.Id)
			{
				return BadRequest();
			}

			var result = await _stockService.UpdateAsync(_mapper.Map<Stock>(updateStockDto));
			
			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _stockService.DeleteAsync(id);

			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}
			 
			return NoContent();
		}
	}
}
