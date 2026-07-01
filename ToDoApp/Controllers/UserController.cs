using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{

    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("get_users/")]
    public async Task<IActionResult> GetUsers()
    {
        ApiResponse<List<ReturnUserDto>> users = await _userService.GetAllUsers();
        return Ok(users);
    }


    [HttpPost("add_user/")]
    public async Task<IActionResult> AddUser(UserDto addUserDto)
    {
        ApiResponse<object> response = await _userService.AddUserAsync(addUserDto);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [HttpPost("login/")]
    public async Task<IActionResult> Login(UserDto loginDto)
    {
        ApiResponse<string> response = await _userService.Login(loginDto);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
