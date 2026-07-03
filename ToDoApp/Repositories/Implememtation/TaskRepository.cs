using Microsoft.EntityFrameworkCore;
using ToDoApp.Common.Dtos;
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


    public async Task<List<TaskUser>> GetAllUserTasksAsync(int pageSize, int page, string? search, string? category, Guid userId)
    {
        IQueryable<TaskUser> query = _context.Tasks
            .AsNoTracking()
            .Include(x => x.Category)
            .Where(t => t.User.Id == userId);

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query
                .Where(t => t.Name.Contains(search));
        }
        if (!string.IsNullOrEmpty(category))
        {
            category = category.ToLower();
            query = query
                .Where(t => t.Category.Name == category);
        }

        List<TaskUser> tasks = await query
            .OrderByDescending(t => t.CreatedDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return tasks;
    }

    public async Task AddCategory(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();  
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


    public async Task AddCategoryAsync(Category category)
    {
        await _context.AddAsync(category);
        await _context.SaveChangesAsync();
    }


    public async Task<Category?> GetCategoryByNameAsync(string name)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
    }


    public async Task<List<Category>> GetUserCategoriesAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.Categories)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Categories)
            .ToListAsync();
    }


    public async Task<int> GetCountOfTasksAsync(string? search, string? category, Guid userId)
    {

        IQueryable<TaskUser> query = _context.Tasks
           .AsNoTracking()
           .Where(t => t.User.Id == userId);

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            query = query
                .Where(t => t.Name.Contains(search));
        }
        if (!string.IsNullOrEmpty(category))
        {
            category = category.ToLower();
            query = query
                .Where(t => t.Name.Contains(category));
        }
        int count = await query
            .CountAsync();

        return count;
    }
}
