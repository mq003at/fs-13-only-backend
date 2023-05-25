namespace store.Services;

using store.Db;
using store.DTOs;
using store.Models;
using store.Services;
using Microsoft.EntityFrameworkCore;

public class DbCategoryService : DbCrudService<Category, CategoryDTO, CategoryFilter, CategoryUpdateDTO>, ICategoryService
{
    public DbCategoryService(AppDbContext dbContext)
        : base(dbContext) { }
}
