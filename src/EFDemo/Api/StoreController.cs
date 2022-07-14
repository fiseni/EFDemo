using AutoMapper;
using AutoMapper.QueryableExtensions;
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
	private readonly IMapper _mapper;

	public StoreController(AppDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	[HttpGet("/stores")]
	public async Task<ActionResult<List<StoreListDto>>> ListAllAsync(CancellationToken cancellationToken)
	{
        var stores = await _dbContext.Stores
			.ProjectTo<StoreListDto>(_mapper.ConfigurationProvider)
			.ToListAsync(cancellationToken);

		var result = _mapper.Map<List<StoreListDto>>(stores);

		return result;
	}

	[HttpGet("/stores/{id}")]
	public async Task<ActionResult<StoreDto>> Get([FromRoute] int id, CancellationToken cancellationToken)
	{
		var store = await _dbContext.Stores
			.Where(x => x.Id == id)
			.ProjectTo<StoreDto>(_mapper.ConfigurationProvider)
			.FirstOrDefaultAsync(cancellationToken);

        var storeDto = _mapper.Map<StoreDto>(store);

		return storeDto;
	}

	[HttpPost("/stores")]
	public async Task<ActionResult<StoreDto>> Post(StoreCreateDto createDto, CancellationToken cancellationToken)
	{
		var store = _mapper.Map<Store>(createDto);

		_dbContext.Stores.Add(store);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return _mapper.Map<StoreDto>(store);
	}

	[HttpPut("/stores/{id}")]
	public async Task<ActionResult<StoreDto>> Update([FromRoute] int id, StoreUpdateDto updateDto, CancellationToken cancellationToken)
	{
		var store = await _dbContext.Stores
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync(cancellationToken);

		if (store is null) return NotFound();

		_mapper.Map(updateDto, store);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return _mapper.Map<StoreDto>(store);
	}

	[HttpDelete("/stores/{id}")]
	public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
	{
		var store = await _dbContext.Stores
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

		if (store is null) return NotFound();

		_dbContext.Stores.Remove(store);

		await _dbContext.SaveChangesAsync(cancellationToken);

		return Ok();
	}
}
