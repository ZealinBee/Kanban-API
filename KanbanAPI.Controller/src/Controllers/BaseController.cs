using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]
[Route("api/[controller]s")]

public class BaseController<T, TCreateDto, TGetDto, TUpdateDto> : ControllerBase
{
    protected readonly IBaseService<TCreateDto, TGetDto, TUpdateDto> _service;
    public BaseController(IBaseService<TCreateDto, TGetDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(statusCode: 201)]
    [ProducesResponseType(statusCode: 400)]
    public virtual async Task<ActionResult<TGetDto>> CreateOneAsync([FromBody] TCreateDto dto)
    {
        try
        {
            var newItem = await _service.CreateOneAsync(dto);
            // should return 201
            return Ok(newItem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    public virtual async Task<ActionResult<TGetDto>> GetOneAsync([FromRoute] Guid id)
    {
        try
        {
            var item = await _service.GetOneAsync(id);
            return Ok(item);
        }
        // item not found exception later
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    public virtual async Task<ActionResult<IEnumerable<TGetDto>>> GetAllAsync()
    {
        try
        {
            var items = await _service.GetAllAsync();
            return Ok(items.ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public virtual async Task<ActionResult<TGetDto>> UpdateOneAsync([FromRoute] Guid id, [FromBody] TUpdateDto dto)
    {
        try
        {
            var item = await _service.GetOneAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            var updatedItem = await _service.UpdateOneAsync(dto, id);
            return Ok(updatedItem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(statusCode: 200)]
    [ProducesResponseType(statusCode: 400)]
    [ProducesResponseType(statusCode: 404)]
    public virtual async Task<ActionResult> DeleteOneAsync([FromRoute] Guid id)
    {
        try
        {
            var item = await _service.GetOneAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await _service.DeleteOneAsync(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}