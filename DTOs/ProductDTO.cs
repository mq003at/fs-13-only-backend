namespace store.DTOs;

using store.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using store.DTOs;
using System.Text.Json.Serialization;

public class ProductDTO : BaseDTO<Product>
{
    [Required(ErrorMessage = "Title must not be blank or empty")]
    public string Title { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Description must not be blank or empty")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category must not be blank or empty")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "Category must be an integer")]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }

    [MinLength(1, ErrorMessage = "At least one image is required")]
    public ICollection<string> Images { get; set; } = new List<string>();

    public override void UpdateModel(Product model)
    {
        model.Title = Title;
        model.Price = Price;
        model.Images = Images;
        model.Description = Description;
        model.CategoryId = CategoryId;
        model.Category = Category ?? model.Category;
    }
}

public class ProductUpdateDTO : BaseDTO<Product>
{
    public string? Title { get; set; }
    public double? Price { get; set; }

    public string? Description { get; set; }
    public int? CategoryId { get; set; }
    public ICollection<string>? Images { get; set; }

    public override void UpdateModel(Product model)
    {
        model.Title = Title ?? model.Title;
        model.Price = Price ?? model.Price;
        model.Images = Images ?? model.Images;
        model.Description = Description ?? model.Description;
        model.CategoryId = CategoryId ?? model.CategoryId;
    }
}

public class ProductInCartItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public int CategoryId { get; set; }
    public ICollection<string> Images { get; set; } = new List<string>();
}
