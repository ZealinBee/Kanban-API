using KanbanAPI.Domain;
using AutoMapper;

namespace KanbanAPI.Business;

public class BaseService<T, TCreateDto, TGetDto, TUpdateDto> : IBaseService<TCreateDto, TGetDto, TUpdateDto>
{
    protected readonly IMapper _mapper;
    protected readonly IBaseRepo<T> _repo;

    public BaseService(IBaseRepo<T> repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public virtual async Task<TGetDto> CreateOneAsync(TCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        var properties = typeof(TCreateDto).GetProperties();
        foreach (var property in properties)
        {
            if (property.GetValue(dto) == null)
                throw new ArgumentNullException($"{property.Name} is null");
        }
        var newItem = _mapper.Map<T>(dto);
        return _mapper.Map<TGetDto>(await _repo.CreateOneAsync(newItem));
    }

    public virtual async Task<TGetDto> GetOneAsync(Guid id, Guid userId)
    {
        var item = await _repo.GetOneAsync(id);
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        return _mapper.Map<TGetDto>(item);
    }

    public virtual async Task<TGetDto> CreateOneAsync(TCreateDto dto, Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<TGetDto> UpdateOneAsync(TUpdateDto dto, Guid id)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));
        var item = await _repo.GetOneAsync(id);
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        var updatedItem = _mapper.Map(dto, item);
        await _repo.UpdateOneAsync(updatedItem);
        return _mapper.Map<TGetDto>(updatedItem);
    }

    public virtual async Task<bool> DeleteOneAsync(Guid id)
    {
        var item = await _repo.GetOneAsync(id);
        if (item == null)
            throw new ArgumentNullException(nameof(item));
        return await _repo.DeleteOneAsync(id);
    }

}