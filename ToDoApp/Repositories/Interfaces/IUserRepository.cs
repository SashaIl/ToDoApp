using Microsoft.EntityFrameworkCore;
using ToDoApp.Db.Entities;

namespace ToDoApp.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid userId);

    public Task<User?> GetUserByEmailAsync(string email);

    public Task<List<User>> GetAllUsersAsync();

    public Task AddUserAsync(User user);
}
