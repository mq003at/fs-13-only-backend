namespace store.DTOs;

using store.Models;
using System.ComponentModel.DataAnnotations;

public class UserSignInDTO
{
    [Required(ErrorMessage = "Email must be provided.")]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password must be provided.")]
    public string Password { get; set; } = null!;
    public string? Purpose { get; set; }
}

public class CredentialDTO
{
    public string Token { get; set; } = null!;
    public DateTime Expiration { get; set; }
    public string? Purpose { get; set; }
}

public class UserSignInResponseDTO
{
    public CredentialDTO AccessToken {get; set;} = null!;
    public CredentialDTO? SpecialToken {get; set;}
    public UserNoPasswordDTO User { get; set; } = null!;
}

public class UserNoPasswordDTO
{
    public int? Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }
    public string? Avatar { get; set; }
    public DateTime CreationAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public UserNoPasswordDTO(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        Role = user.Role;
        Avatar = user.Avatar;
        CreationAt = user.CreationAt;
        UpdatedAt = user.UpdatedAt;
    }
}

public class UserSignUpDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [EmailAddressAttribute]
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public string Avatar { get; set; } = string.Empty;
    public string? Purpose { get; set; }
}

public class UserSignUpResponseDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Role Role { get; set; }
    public Cart? Cart { get; set; } = null!;
}

public class UserDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;
    public string? Avatar { get; set; } = string.Empty;
    public DateTime CreationAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public UserDTO(User user)
    {
        this.Id = user.Id;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.Email = user.Email;
        this.Password = user.Password;
        this.Role = user.Role;
        this.Avatar = user.Avatar;
        this.CreationAt = user.CreationAt;
        this.UpdatedAt = user.UpdatedAt;
    }
}

public class UserUpdateDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Avatar { get; set; }

    public void UpdateModel(User model)
    {
        model.FirstName = FirstName ?? model.FirstName;
        model.LastName = LastName ?? model.LastName;
        model.Email = Email ?? model.Email;
        model.Password = Password ?? model.Password;
        model.Avatar = Avatar ?? model.Avatar;
    }
}

public class isRegisteredDTO 
{
    public string Email { get; set; } = null!;
}
