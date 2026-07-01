using Microsoft.EntityFrameworkCore;
using ToDoApp.Db;
using ToDoApp.Db.Entities;
using ToDoApp.Repositories.Interfaces;

namespace ToDoApp.Repositories.Implememtation;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;
    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<TaskUser?> GetTaskByIdAsync(Guid taskId, Guid userId)
    {
        TaskUser? task = await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(task => task.Id == taskId && task.User.Id == userId);
        return task;
    }


    public List<TaskUser> GetAllUserTasks(Guid userId)
    {
        List<TaskUser> tasks = _context.Tasks.AsNoTracking().Where(t => t.UserId == userId).ToList();
        return tasks;
    }

    public async Task AddTaskAsync(TaskUser task, Guid userId)
    {
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaskAsync(Guid taskId, Guid userId)
    {
        await _context.Tasks
            .Where(t => t.Id == taskId && t.User.Id == userId)
            .ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaskAsync(TaskUser task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }
}
