namespace store.Models;

using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

public class Product : BaseModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public double Price { get; set; } = 0.0;

    [JsonIgnore]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    [Column(TypeName = "jsonb")]
    public ICollection<string> Images { get; set; } = null!;

    [JsonIgnore]
    public ICollection<CartItem>? CartItems { get; set; } = null!;
}
