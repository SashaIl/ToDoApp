using Microsoft.EntityFrameworkCore;
using ToDoApp.Db;
using ToDoApp.Db.Entities;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories.Implememtation;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetUserByIdAsync(Guid userId)
    {
        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);
        return user;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }


    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> users = await _context.Users
            .Include(u => u.Tasks)
            .AsNoTracking()
            .ToListAsync();
        return users;
    }


    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

}
