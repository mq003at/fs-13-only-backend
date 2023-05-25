namespace store.Controllers;

using store.Services;
using store.Models;
using store.DTOs;
using store.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public abstract class CrudController<TModel, TDto, TFilter, TUpdateDTO> : ApiControllerBase
    where TModel : BaseModel
    where TDto : BaseDTO<TModel>
    where TFilter : BaseFilter<TModel>
    where TUpdateDTO : BaseDTO<TModel>
{
    protected readonly ICrudService<TModel, TDto, TFilter, TUpdateDTO> _service;

    public CrudController(ICrudService<TModel, TDto, TFilter, TUpdateDTO> service)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    [Authorize(Policy = "Admin")]
    [HttpPost]
    public async virtual Task<IActionResult> Create(TDto request)
    {
        var item = await _service.CreateAsync(request);
        if (item is null)
        {
            return BadRequest();
        }
        return Ok(item);
    }

    [HttpGet("{id:int}")]
    public async virtual Task<ActionResult> Get(int id)
    {
        var item = await _service.GetAsync(id);
        if (item is null)
        {
            return NotFound("Item is not found");
        }
        return Ok(item);
    }

    [Authorize(Policy = "Admin")]
    [HttpPut("{id:int}")]
    public async virtual Task<ActionResult<TModel?>> Update(int id, TUpdateDTO request)
    {
        var item = await _service.UpdateAsync(id, request);
        if (item is null)
        {
            return NotFound("Item is not found");
        }
        return Ok(item);
    }

    [Authorize(Policy = "Admin")]
    [HttpDelete("{id}")]
    public async virtual Task<ActionResult> Delete(int id)
    {
        if (await _service.DeleteAsync(id))
        {
            return Ok(new { Message = "Item is deleted " });
        }
        return NotFound("Item is not found.");
    }

    [HttpGet]
    public async virtual Task<ActionResult<IEnumerable<TDto>>> GetAllAsync(
        [FromQuery] TFilter filter
    )
    {
        var items = await _service.GetAllAsync(filter);
        if (items == null || !items.Any())
        {
            return BadRequest(new { message = "No entry found, or that table is empty." });
        }
        return Ok(items);
    }
}
