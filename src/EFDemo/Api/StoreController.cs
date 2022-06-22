using EFDemo.Api.Models;
using EFDemo.Domain;
using EFDemo.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Api;

[ApiController]
public class StoreController : ControllerBase
{
	private readonly AppDbContext _dbContext;

	public StoreController(AppDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	[HttpGet("/stores")]
	public async Task<ActionResult<List<StoreListDto>>> ListAllAsync(CancellationToken cancellationToken)
	{
		var stores = await _dbContext.Stores
			.Select(x => new StoreListDto
			{
				Id = x.Id,
				Name = x.Name
			})
			.ToListAsync(cancellationToken);

		return stores;
	}

	[HttpGet("/stores/{id}")]
	public async Task<ActionResult<StoreDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
	{
		//var store = await _dbContext.Stores
		//	.Include(x=>x.Products)
		//	.Where(x => x.Products.Any(p => p.Id == id))
		//	.FirstOrDefaultAsync(cancellationToken);

		var storeDto = await _dbContext.Stores
			.Where(x => x.Id == id)
			.Select(x => new StoreDto
			{
				Id = x.Id,
				Name = x.Name,
				Products = x.Products.Select(p => new ProductDto
				{
					Name = p.Name
				}).ToList()
			})
			.FirstOrDefaultAsync(cancellationToken);

		if (storeDto is null) return NotFound();

		return storeDto;
	}

	[HttpPost("/stores")]
	public async Task<ActionResult<StoreDto>> Post(StoreCreateDto createDto, CancellationToken cancellationToken)
	{
		var store = new Store
		{
			Name = createDto.Name
		};

		_dbContext.Stores.Add(store);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return new StoreDto
		{
			Id = store.Id,
			Name = store.Name
		};
	}

	[HttpPut("/stores/{id}")]
	public async Task<ActionResult<StoreDto>> Update([FromRoute] int id, StoreUpdateDto updateDto, CancellationToken cancellationToken)
	{
		var store = await _dbContext.Stores.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

		if (store is null) return NotFound();

		store.Name = updateDto.Name;

		await _dbContext.SaveChangesAsync(cancellationToken);

		return new StoreDto
		{
			Id = store.Id,
			Name = store.Name
		};
	}

	[HttpDelete("/stores/{id}")]
	public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
	{
		var store = await _dbContext.Stores.Where(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);

		if (store is null) return NotFound();

		_dbContext.Stores.Remove(store);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Ok();
	}
}
