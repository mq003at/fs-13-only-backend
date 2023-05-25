namespace store.DTOs;

using store.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using store.DTOs;

public class CategoryDTO : BaseDTO<Category>
{
    [Required(ErrorMessage = "Name must not be blank or empty")]
    public string Name { get; set; } = string.Empty;

    [MinLength(1, ErrorMessage = "At least one image is required")]
    public ICollection<string> Images { get; set; } = new List<string>();

    public override void UpdateModel(Category model)
    {
        model.Name = Name;
        model.Images = Images;
    }
}

public class CategoryUpdateDTO : BaseDTO<Category>
{
    public string? Name { get; set; } 
    public ICollection<string>? Images { get; set; } 

    public override void UpdateModel(Category model)
    {
        model.Name = Name ?? model.Name;
        model.Images = Images ?? model.Images;
    }
}
