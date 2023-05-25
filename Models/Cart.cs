namespace store.Models;

using System;
using System.Text.Json.Serialization;

public class Cart : BaseModel
{
    [JsonIgnore]
    public int UserId { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}

