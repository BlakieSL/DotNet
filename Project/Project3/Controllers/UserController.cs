using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project3.DTOs;
using Project3.Models;
using Project3.Services;
using RegisterRequest = Project3.DTOs.RegisterRequest;

namespace Project3.Controllers;

[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterStudent(RegisterRequest model)
    {
        try
        {
            await _userService.RegisterStudent(model);
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var tokens = await _userService.Login(loginRequest);
        return Ok(new
        {
            accessToken = tokens[0],
            refreshToken = tokens[1]
        });
    }
}