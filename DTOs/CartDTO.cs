namespace store.DTOs;

using store.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using store.DTOs;

public class CartDTO : BaseDTO<Cart>
{
    public ICollection<CartItem>? CartItems { get; set; } = null!;
    public ICollection<int>? CartItemsArray { get; set; } = null!;
    public int UserId { get; set; }

    public override void UpdateModel(Cart model)
    {
        model.CartItems = CartItems ?? model.CartItems;
        model.UserId = UserId;
    }
}

public class CartUpdateDTO : BaseDTO<Cart>
{
    public ICollection<CartItem>? CartItems { get; set; } = null!;
    public ICollection<int>? CartItemsArray { get; set; } = null!;

    public override void UpdateModel(Cart model)
    {
        model.CartItems = CartItems ?? model.CartItems;
    }
}

public class CartResponseDTO : BaseModel
{
    public ICollection<CartItemResponseDTO>? CartItems { get; set; } = null!;
    public CartResponseDTO(Cart cart, ICollection<CartItemResponseDTO> cartItems)
    {
        CartItems = cartItems;
        Id = cart.Id;
        UpdatedAt = cart.UpdatedAt;
        CreationAt = cart.CreationAt;
    }
}
