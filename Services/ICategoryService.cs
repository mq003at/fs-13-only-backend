namespace store.Services;

using store.Models;
using store.DTOs;
using store.Services;

public interface ICategoryService : ICrudService<Category, CategoryDTO, CategoryFilter, CategoryUpdateDTO>
{
}
