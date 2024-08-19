using Microsoft.AspNetCore.Mvc;
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
		public async Task<ActionResult> Create(Toy toy)
		{
			try
			{
				_toyService.AddAsync(toy);
				
				await _uow.Commit();
			}
			catch (Exception)
			{

				await _uow.Rollback();
			}
			
			return CreatedAtAction(nameof(GetById), new { id = toy.Id }, toy);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, Toy toy)
		{
			if (id != toy.Id)
			{
				return BadRequest();
			}

			try
			{
				await _toyService.UpdateAsync(toy);

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
			var toy = await _toyService.GetByIdAsync(id);
			if (toy == null)
			{
				return NotFound();
			}

			try
			{
				await _toyService.DeleteAsync(id);
				
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
