namespace store.Services;

using store.Db;
using store.DTOs;
using store.Models;
using store.Services;

using Microsoft.EntityFrameworkCore;

public class DbProductService
    : DbCrudService<Product, ProductDTO, ProductFilter, ProductUpdateDTO>,
        IProductService
{
    public DbProductService(AppDbContext dbContext)
        : base(dbContext) { }

    public override async Task<ICollection<Product>?> GetAllAsync(ProductFilter? filter)
    {
        var productQueries = await base.GetAllAsync(filter);
        if (productQueries != null)
        {
            foreach (var product in productQueries)
            {
                var category = await _dbContext.Category.FindAsync(product.CategoryId);
                product.Category = category;
            }
        }
        return productQueries;
    }

    public override async Task<Product?> CreateAsync(ProductDTO request)
    {
        var product = await base.CreateAsync(request);
        if (product is null)
        {
            return null;
        }
        // Explicit loading
        await _dbContext.Entry(product).Reference(c => c.Category).LoadAsync();
        return product;
    }

    public override async Task<Product?> UpdateAsync(int id, ProductUpdateDTO request)
    {
        var product = await GetAsync(id);
        if (product is null)
        {
            return null;
        }
        request.UpdateModel(product);
        await _dbContext.Entry(product).Reference(c => c.Category).LoadAsync();
        await _dbContext.SaveChangesAsync();
        return product;
    }
}
