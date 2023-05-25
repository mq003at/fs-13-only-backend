namespace store.Services;

using store.Db;
using store.DTOs;
using store.Models;
using store.Services;
using Microsoft.EntityFrameworkCore;

public class DbCartItemService
    : DbCrudService<CartItem, CartItemDTO, CartItemFilter, CartItemUpdateDTO>,
        ICartItemService
{
    public DbCartItemService(AppDbContext dbContext)
        : base(dbContext) { }

    public async Task<CartItem?> HandleCartItem(CartItemUpdateDTO request, int id)
    {
        var existingCartItem = await _dbContext.CartItems?.FirstOrDefaultAsync(
            ci => ci.ProductId == request.ProductId && ci.CartId == id
        );

        if (existingCartItem != null)
        {
            if (request.Quantity == 0)
            {
                _dbContext.CartItems.Remove(existingCartItem);
                await _dbContext.SaveChangesAsync();
                return null;
            }

            existingCartItem.Quantity = request.Quantity;
            await _dbContext.Entry(existingCartItem).Reference(ci => ci.Product).LoadAsync();
            await _dbContext.Entry(existingCartItem.Product).Reference(c => c.Category).LoadAsync();
            await _dbContext.SaveChangesAsync();
            return existingCartItem;
        }

        if (request.Quantity == 0) return null;

        var product = await _dbContext.Products.FindAsync(request.ProductId);
        if (product is null)
        {
            return null;
        }
        var newRequest = new CartItemDTO(product, request.Quantity, id);

        return await base.CreateAsync(newRequest);
    }
}
