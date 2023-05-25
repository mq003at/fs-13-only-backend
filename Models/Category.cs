namespace store.Models;

using System.ComponentModel.DataAnnotations.Schema;

public class Category : BaseModel
{
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "jsonb")]
    public ICollection<string> Images { get; set; } = null!;
}
