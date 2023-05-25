namespace store.Services;

using store.DTOs;
using store.Models;

public interface ITokenService
{
    Task<CredentialDTO> GenerateTokenAsync(User user, string? purpose);
    
}