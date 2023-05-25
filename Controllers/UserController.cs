namespace store.Controllers;

using store.Services;
using store.DTOs;
using store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class UserController : ApiControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(UserSignUpDTO request)
    {
        var res = await _service.SignUpAsync(request);

        if (res is string errors)
        {
            return BadRequest(errors);
        }
        return Ok(res);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(UserSignInDTO request)
    {
        var res = await _service.SignInAsync(request);
        if (res is string errors)
        {
            return Unauthorized(errors);
        }
        return Ok(res);
    }

    [HttpPost("auto-signin")]
    public async Task<IActionResult> AutoSignIn()
    {
        string? authToken = Request.Headers["Authorization"];
        if (authToken is null || !authToken.StartsWith("Bearer "))
        {
            return Unauthorized();
        }
        else
        {
            var res = await _service.AutoSignInAsync(authToken.Substring(7));
            if (res is null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(res);
            }
        }
    }

    [HttpPost("is-registered")]
    public async Task<IActionResult> IsRegistered(isRegisteredDTO request)
    {
        var res = await _service.IsRegisteredAsync(request);
        return Ok(new { IsRegistered = res });
    }

    [Authorize(Policy = "UserMatching")]
    [HttpDelete("{id:int}/delete")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var res = await _service.DeleteAsync(id);
        if (res is null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(res);
        }
    }

    [Authorize(Policy = "UserMatching")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateDTO request)
    {
        var res = await _service.UpdateAsync(id, request);
        if (res is null)
        {
            return BadRequest();
        }
        else
        {
            return Ok(res);
        }
    }
}
