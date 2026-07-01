using Microsoft.EntityFrameworkCore;
using ToDoApp.Db.Entities;

namespace ToDoApp.Db;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context)
        : base(context) 
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<TaskUser> Tasks { get; set; }

}
