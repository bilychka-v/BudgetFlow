using Application.Interfaces;
using BudgetFlow.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BudgetFlow.Controllers;
[ApiController]
[Route("api/[controller]")]

public class UserController:ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerRequest)
    {
        var userRegister = await _userService.Register
        (
            registerRequest.UserName,
            registerRequest.Email,
            registerRequest.Password
        );
        
        return Ok(userRegister);
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequst loginRequest)
    {
        var token = await _userService.Login(loginRequest.Email, loginRequest.Password);
        return Ok(token);
    }
}