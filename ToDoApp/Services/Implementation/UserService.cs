using AutoMapper;
using Azure.Core;
using BCrypt.Net;
using System.Text.RegularExpressions;
using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Implementation;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;
    private readonly int _cryptoSalt;

    public UserService(IUserRepository userRepository, IJwtService jwtService, IMapper mapper, IConfiguration conf)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _mapper = mapper;
        _cryptoSalt = conf.GetValue<int>("CryptoSalt");
    }


    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, _cryptoSalt);
    }


    public async Task<ApiResponse<List<ReturnUserDto>>> GetAllUsers()
    {
        try
        {
            List<User> users = await _userRepository.GetAllUsersAsync();
            List<ReturnUserDto> returnUsers = _mapper.Map<List<ReturnUserDto>>(users);
            return ApiResponse<List<ReturnUserDto>>.Success(returnUsers);
        }
        catch(ArgumentNullException ex)
        {
            return ApiResponse<List<ReturnUserDto>>.Fail("users is null");
        }
    }


    public async Task<ApiResponse<object>> AddUserAsync(UserDto userDto)
    {
        try
        {

            bool isUserExists = await _userRepository.GetUserByEmailAsync(userDto.Email) != null;

            if (isUserExists)
            {
                return ApiResponse<object>.Fail("User already exists");
            }

            if (string.IsNullOrEmpty(userDto.Pass) || string.IsNullOrEmpty(userDto.Email))
            {
                return ApiResponse<object>.Fail("Email or password cannot be empty");
            }

            if(userDto.Pass.Length < 6)
            {
                return ApiResponse<object>.Fail("Password must be at least 6 characters long");
            }   

            User user = _mapper.Map<UserDto, User>(userDto);

            user.HashedPass = HashPassword(userDto.Pass);

            await _userRepository.AddUserAsync(user);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }


    public async Task<ApiResponse<string>> Login(UserDto userDto)
    {
        try
        {
            User? user = await _userRepository.GetUserByEmailAsync(userDto.Email);
            bool isUserExists = user != null;
            if (!isUserExists)
            {
                return ApiResponse<string>.Fail("User not found");
            }

            if(!BCrypt.Net.BCrypt.Verify(userDto.Pass, user!.HashedPass) && userDto.Email != user.Email)
            {
                return ApiResponse<string>.Fail("Invalid email or password");
            }

            string token = _jwtService.GenerateToken(user);

            return ApiResponse<string>.Success(token);
        }
        catch (Exception ex)
        {
            return ApiResponse<string>.Fail(ex.Message);
        }
    }
}
