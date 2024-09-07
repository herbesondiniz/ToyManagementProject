using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain;
using ToyManagementProject.Domain.DTOs;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;
using ToyManagementProject.Infra.Data.UoW;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class ToysController : ControllerBase
	{
		private readonly IToyService _toyService;
		private readonly IUnitOfWork _uow;

		public ToysController(IToyService toyService, IUnitOfWork uow)
		{
			_toyService = toyService;
			_uow = uow;
		}
		[HttpGet]
		public async Task<ActionResult<List<Toy>>> GetAll()
		{
			var toys = await _toyService.GetAllAsync();
			return Ok(toys);
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
		public async Task<ActionResult> Create(ToyDTO toyDTO)
		{
			var result = await _toyService.AddAsync(toyDTO);
			
			if (!result.IsSuccess) 
			{
				return UnprocessableEntity(result.Errors);
			}

			return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, ToyDTO toy)
		{			
			if (id != toy.Id)
			{
				return BadRequest();
			}
			var result = await _toyService.UpdateAsync(toy);

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
