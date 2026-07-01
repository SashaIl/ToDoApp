using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;

namespace ToDoApp.Services.Interfaces;

public interface IUserService
{
    public Task<ApiResponse<List<ReturnUserDto>>> GetAllUsers();
    public Task<ApiResponse<object>> AddUserAsync(UserDto userDto);

    public Task<ApiResponse<string>> Login(UserDto userDto);
}
