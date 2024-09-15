using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;
using ToyManagementProject.Services.Dtos.Toy;

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
			var result = await _toyService.GetAllAsync();
			if (!result.IsSuccess) 
			{
				UnprocessableEntity(result.Errors);
			}
			
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ToyDto>> GetById(int id)
		{
			var result = await _toyService.GetByIdAsync(id);
			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> Create(CreateToyDto createToyDTO)
		{
			var result = await _toyService.AddAsync(_mapper.Map<Toy>(createToyDTO));
			
			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, UpdateToyDto updateToyDto)
		{			
			if (id != updateToyDto.Id)
			{
				return BadRequest();
			}
			var result = await _toyService.UpdateAsync(_mapper.Map<Toy>(updateToyDto));

			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var result = await _toyService.DeleteAsync(id);
			
			if (!result.IsSuccess)
			{
				return UnprocessableEntity(result.Errors);
			}

			return NoContent();
		}
	}
}
