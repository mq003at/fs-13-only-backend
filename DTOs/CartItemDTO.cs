namespace store.DTOs;

using store.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using store.DTOs;
using System.Text.Json.Serialization;

public class CartItemDTO : BaseDTO<CartItem>
{
    public Product? Product { get; set; } = null!;

    [JsonIgnore]
    public int? ProductId { get; set; } = null;

    [Required(ErrorMessage = "Require number of product. If it reaches 0, delete the CartItem.")]
    public int Quantity { get; set; }
    public int CartId { get; set; }

    public override void UpdateModel(CartItem model)
    {
        // model.Product = Product ?? model.Product;
        model.CartId = CartId;
        model.Quantity = Quantity;
        model.ProductId = ProductId ?? model.ProductId;
    }

    public CartItemDTO(Product product, int quantity, int id)
    {
        CartId = id;
        Product = product;
        ProductId = product.Id;
        Quantity = quantity;
    }
}

public class CartItemUpdateDTO : BaseDTO<CartItem>
{
    [Required(ErrorMessage = "Require ProductID.")]
    public int ProductId { get; set; }

    [Required(
        ErrorMessage = "Require number of product. Instead of reaching 0, use Delete instead."
    )]
    public int Quantity { get; set; }
    public int CartId { get; set; }

    public override void UpdateModel(CartItem model)
    {
        model.Quantity = Quantity;
    }
}

public class CartItemResponseDTO
{
    public int Id { get; set; }
    public ProductInCartItemDTO Product { get; set; } = null!;
    public int Quantity { get; set; }

    public CartItemResponseDTO(CartItem item)
    {
        Id = item.Id;
        Product = new ProductInCartItemDTO
        {
            Id = item.Product.Id,
            Title = item.Product.Title,
            Description = item.Product.Description,
            Price = item.Product.Price,
            CategoryId = item.Product.CategoryId,
            Images = item.Product.Images
        };
        Quantity = item.Quantity;
    }
}
