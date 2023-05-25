namespace store.Services;

using store.Db;
using store.DTOs;
using store.Models;
using store.Services;
using Microsoft.EntityFrameworkCore;

public class DbCartService : DbCrudService<Cart, CartDTO, CartFilter, CartUpdateDTO>, ICartService
{
    public DbCartService(AppDbContext dbContext)
        : base(dbContext) { }

    public new async Task<object?> GetAsync(int id)
    {
        var cart = await _dbContext
            .Set<Cart>()
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (cart == null)
        {
            return null;
        }

        if (cart.CartItems != null)
        {
            var resCartItems = new List<CartItemResponseDTO>();
            cart.CartItems = cart.CartItems.OrderBy(ci => ci.Product.Title).ToList();

            foreach (var cartItem in cart.CartItems)
            {
                var cartItemRes = new CartItemResponseDTO(cartItem);
                resCartItems.Add(cartItemRes);
            }

            var returnCart = new CartResponseDTO(cart, resCartItems);

            return returnCart;
        }
        return null;
    }
}
