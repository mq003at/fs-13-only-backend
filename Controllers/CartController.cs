namespace store.Controllers;

using store.Services;
using store.Models;
using store.DTOs;
using store.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public class CartController : CrudController<Cart, CartDTO, CartFilter, CartUpdateDTO>
{
    private readonly ICartService _CartService;
    private readonly ICartItemService _cartItemService;

    public CartController(ICartService cartService, ICartItemService cartItemService)
        : base(cartService)
    {
        _CartService = cartService;
        _cartItemService = cartItemService;
    }

    [HttpGet]
    public override async Task<ActionResult<IEnumerable<CartDTO>>> GetAllAsync(
        [FromQuery] CartFilter filter
    )
    {
        await Task.CompletedTask;
        return Unauthorized("No one is allowed to retrieve all Carts.");
    }

    [HttpPost]
    public override async Task<IActionResult> Create(CartDTO request)
    {
        await Task.CompletedTask;
        return Unauthorized("Cart must be created when user signing up");
    }

    [Authorize(Policy = "UserMatching")]

    [HttpGet("{id:int}")]
    public override async Task<ActionResult> Get(int id)
    {
        var response = await _CartService.GetAsync(id);
        if (response is null)
        {
            return BadRequest();
        }
        else
            return Ok(response);
    }

    [Authorize(Policy = "UserMatching")]
    [HttpPost("{id:int}/add-item")]
    public async Task<ActionResult<Cart?>> CreateCartItem(
        int id,
        [FromBody] CartItemUpdateDTO request
    )
    {
        var cartItem = await _cartItemService.HandleCartItem(request, id);
        if (cartItem is null)
        {
            return BadRequest("There are some errors creating transaction.");
        }

        var cart = await _CartService.GetAsync(id);
        if (cart is null)
        {
            return BadRequest("Errors generating user's cart");
        }

        return Ok(cart);
    }
}
