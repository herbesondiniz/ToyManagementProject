using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class ToysController : ControllerBase
	{
		private readonly IToyService _toyService;
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;

		public ToysController(IToyService toyService, IUnitOfWork uow, IMapper mapper)
		{
			_toyService = toyService;
			_uow = uow;
			_mapper = mapper;
		}
		[HttpGet]
		public async Task<ActionResult<List<ToyDto>>> GetAll()
		{
			var toysDto = await _toyService.GetAllAsync();
			return Ok(toysDto);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Toy>> GetById(int id)
		{
			var toy = await _toyService.GetByIdAsync(id);
			if (toy == null)
			{
				return NotFound();
			}
			return Ok(toy);
		}

		[HttpPost]
		public async Task<ActionResult> Create(ToyDto toyDTO)
		{
			var result = await _toyService.AddAsync(_mapper.Map<Toy>(toyDTO));
			
			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, ToyDto toyDto)
		{			
			if (id != toyDto.Id)
			{
				return BadRequest();
			}
			var result = await _toyService.UpdateAsync(_mapper.Map<Toy>(toyDto));

			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _toyService.DeleteAsync(id);
			
			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return Ok(result.Data);
		}
	}
}
