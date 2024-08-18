using Microsoft.AspNetCore.Mvc;
using ToyManagementProject.Domain.Entities;
using ToyManagementProject.Domain.Interfaces.Services;

namespace ToyManagementProject.API.Controllers
{
	[ApiController]
	[Route("[controller]")]	
	public class ToysController : ControllerBase
	{
		private readonly IToyService _toyService;

		public ToysController(IToyService toyService)
		{
			_toyService = toyService;
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
		public async Task<ActionResult> Create(Toy toy)
		{
			await _toyService.AddAsync(toy);
			return CreatedAtAction(nameof(GetById), new { id = toy.Id }, toy);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Toy toy)
		{
			if (id != toy.Id)
			{
				return BadRequest();
			}

			await _toyService.UpdateAsync(toy);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			var toy = await _toyService.GetByIdAsync(id);
			if (toy == null)
			{
				return NotFound();
			}

			await _toyService.DeleteAsync(id);
			return NoContent();
		}
	}
}
