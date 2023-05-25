namespace store.Models;

using System;
using System.Text.Json.Serialization;
using store.DTOs;

public class CartItem : BaseModel
{
    [JsonIgnore]
    public int ProductId { get; set; }

    [JsonIgnore]
    public int CartId { get; set; }

    public Product Product { get; set; } = null!;

    [JsonIgnore]
    public Cart? Cart { get; set; }
    public int Quantity { get; set; }
}
