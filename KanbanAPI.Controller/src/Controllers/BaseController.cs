using KanbanAPI.Business;
using KanbanAPI.Domain;

namespace KanbanAPI.Controller;

[ApiController]
[Route("api/[controller]s")]

public class BaseController : ControllerBase
{
    protected readonly IBaseService<TCreateDto, TGetDto, TUpdateDto> _service;
    public BaseController(IBaseService<TCreateDto, TGetDto, TUpdateDto> service)
    {
        _service = service;
    }

    [HttpPost]
    public virtual async Task<ActionResult<TGetDto>> CreateOneAsync([FromBody] TCreateDto dto)
    {
        try
        {
            var newItem = await _service.CreateOneAsync(dto);
            return CreatedAtAction(newItem);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TGetDto>> GetOneAsync(Guid id)
    {
        try
        {
            var item = await _service.GetOneAsync(id);
            return Ok(item);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
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
}