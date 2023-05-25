namespace store.Services;

using store.Db;
using store.DTOs;
using store.Models;
using store.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

public class DbUserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ITokenService _tokenService;
    private readonly ICartService _cartService;
    private readonly Dictionary<Role, int> _roleIds = new Dictionary<Role, int>();

    public DbUserService(
        UserManager<User> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        ICartService cartService,
        ITokenService tokenService
    )
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenService = tokenService;
        _cartService = cartService;

        foreach (Role role in Enum.GetValues(typeof(Role)))
        {
            _roleIds[role] = (int)role;
        }
    }

    public async Task<object> SignUpAsync(UserSignUpDTO request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = "U" + request.Email,
            Email = request.Email,
            Role = request.Role,
            Avatar = string.Empty,
        };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            IEnumerable<string> errorsList = result.Errors.Select(e => e.Description);
            string errorString = string.Join("", errorsList);
            return errorString;
        }
        else
        {
            var cartRequest = new CartDTO { UserId = user.Id };

            var cart = await _cartService.CreateAsync(cartRequest);
            if (cart is null)
            {
                return "System Error when creating a cart for user.";
            }
            else
            {
                await _userManager.AddToRoleAsync(user, request.Role.ToString());
                var token = await _tokenService.GenerateTokenAsync(user, null);
                var returnedUser = new UserNoPasswordDTO(user);

                var response = new UserSignInResponseDTO
                {
                    User = returnedUser,
                    AccessToken = token,
                };

                if (request.Purpose != null)
                {
                    var specialToken = await _tokenService.GenerateTokenAsync(
                        user,
                        request.Purpose
                    );
                    response.SpecialToken = specialToken;
                }
                ;
                return response;
            }
        }
    }

    public async Task<object> SignInAsync(UserSignInDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return "Cannot find email address. Please sign up with that email address first.";
        }
        var isPasswordInvalid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isPasswordInvalid)
        {
            return "Incorrect password.";
        }

        var token = await _tokenService.GenerateTokenAsync(user, null);
        var returnedUser = new UserNoPasswordDTO(user);
        var response = new UserSignInResponseDTO { User = returnedUser, AccessToken = token, };

        if (request.Purpose != null)
        {
            var specialToken = await _tokenService.GenerateTokenAsync(user, request.Purpose);
            response.SpecialToken = specialToken;
        }
        ;

        return response;
    }

    public async Task<object?> AutoSignInAsync(string request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var JwtSecurityToken = tokenHandler.ReadJwtToken(request);

        var userId = JwtSecurityToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);
        if (userId == null)
        { 
            return null;
        }

        var user = await _userManager.FindByIdAsync(userId.Value);
        if (userId == null)
        {
            return null;
        }

        var token = await _tokenService.GenerateTokenAsync(user, null);
        var returnedUser = new UserNoPasswordDTO(user);
        var response = new UserSignInResponseDTO { User = returnedUser, AccessToken = token };
        return response;
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return null;
        }

        await _userManager.DeleteAsync(user);
        return user;
    }

    public async Task<User?> UpdateAsync(int id, UserUpdateDTO request)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return null;
        }

        request.UpdateModel(user);
        await _userManager.UpdateAsync(user);
        return user;
    }

    public async Task<bool> IsRegisteredAsync(isRegisteredDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email.ToUpper());
        return user != null;
    }
}
