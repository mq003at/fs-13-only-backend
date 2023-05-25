namespace store.Models;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

public enum Role
{
    Unassigned,
    Admin,
    User
}

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public string? Avatar { get; set; } = string.Empty;
    public Cart? Cart { get; set; }

    [Column("creation_at")]
    public DateTime CreationAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
