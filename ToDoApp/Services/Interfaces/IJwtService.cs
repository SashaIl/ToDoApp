using ToDoApp.Db.Entities;

namespace ToDoApp.Services.Interfaces;

public interface IJwtService
{
    public string GenerateToken(User user);
}
