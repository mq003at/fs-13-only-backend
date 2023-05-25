namespace store.Services;

using store.Models;
using store.DTOs;
using store.Services;

public interface ICartItemService : ICrudService<CartItem, CartItemDTO, CartItemFilter, CartItemUpdateDTO>
{
    Task<CartItem?> HandleCartItem(CartItemUpdateDTO request, int id);
}
