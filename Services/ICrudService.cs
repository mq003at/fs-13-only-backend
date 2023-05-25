namespace store.Services;

using store.Models;

public interface ICrudService<TModel, TDto, TCrudFilter, TUpdateDto>
{
    Task<TModel?> CreateAsync(TDto request);
    Task<TModel?> GetAsync(int id);
    Task<TModel?> UpdateAsync(int id, TUpdateDto request);
    Task<bool> DeleteAsync(int id);
    Task<ICollection<TModel>?> GetAllAsync(TCrudFilter? filter);
}