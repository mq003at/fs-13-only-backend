namespace store.Services;

using store.Models;
using store.DTOs;
using store.Services;

public interface IUserService
{
    Task<object> SignUpAsync(UserSignUpDTO request);
    Task<object> SignInAsync(UserSignInDTO request);
    Task<User?> DeleteAsync(int id);
    Task<User?> UpdateAsync(int id, UserUpdateDTO request);
    Task<bool> IsRegisteredAsync(isRegisteredDTO request);
    Task<object?> AutoSignInAsync(string request);
}
