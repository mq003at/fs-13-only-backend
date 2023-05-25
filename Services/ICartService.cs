namespace store.Services;

using store.Models;
using store.DTOs;
using store.Services;

public interface ICartService : ICrudService<Cart, CartDTO, CartFilter, CartUpdateDTO>
{
    new Task<object?> GetAsync(int id);
}
