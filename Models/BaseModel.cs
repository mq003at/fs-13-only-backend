namespace store.Models;

using System.ComponentModel.DataAnnotations.Schema;

public abstract class BaseModel
{
    public int Id { get; set; }

    [Column("creation_at")]
    public DateTime CreationAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}