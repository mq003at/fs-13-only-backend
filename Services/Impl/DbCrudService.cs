namespace store.Services;

using store.Models;
using store.DTOs;
using store.Db;
using store.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class DbCrudService<TModel, TDto, TCrudFilter, TUpdateDto>
    : ICrudService<TModel, TDto, TCrudFilter, TUpdateDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
    where TCrudFilter : BaseFilter<TModel>
    where TUpdateDto : BaseDTO<TModel>
{
    protected readonly AppDbContext _dbContext;

    public DbCrudService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TModel?> CreateAsync(TDto request)
    {
        var item = new TModel();
        request.UpdateModel(item);
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync(); // Tell the db context to update the database
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        if (item is null)
        {
            return false;
        }
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public virtual async Task<TModel?> GetAsync(int id)
    {
        return await _dbContext.Set<TModel>().FindAsync(id);
    }

    public virtual async Task<TModel?> UpdateAsync(int id, TUpdateDto request)
    {
        var item = await GetAsync(id);
        if (item is null)
        {
            return null;
        }
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }

    public virtual async Task<ICollection<TModel>?> GetAllAsync(TCrudFilter? filter)
    {
        IQueryable<TModel> query = _dbContext.Set<TModel>().AsNoTracking();
        if (filter != null)
        {
            filter.ApplyFilter(ref query);
        }
        else
        {
            query = query.OrderBy(x => x.Id);
        }
        return await query.ToListAsync();
    }
}
