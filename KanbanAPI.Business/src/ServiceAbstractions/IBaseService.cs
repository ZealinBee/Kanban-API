using KanbanAPI.Domain;

namespace KanbanAPI.Business;
public interface IBaseService<TCreateDto, TGetDto, TUpdateDto>
{
    Task<TGetDto> CreateOneAsync(TCreateDto dto);
    Task<TGetDto> CreateOneAsync(TCreateDto dto, Guid id);
    Task<TGetDto> UpdateOneAsync(TUpdateDto dto, Guid id);
    Task<bool> DeleteOneAsync(Guid id);
    Task<TGetDto> GetOneAsync(Guid id);
}