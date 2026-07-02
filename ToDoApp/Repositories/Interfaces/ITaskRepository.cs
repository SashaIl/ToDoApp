using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Db.Entities;

namespace ToDoApp.Repositories.Interfaces;

public interface ITaskRepository
{
    public Task<TaskUser?> GetTaskByIdAsync(Guid taskId, Guid userId);

    public Task<List<TaskUser>> GetAllUserTasksAsync(int pageSize, int page, string? search, string? category, Guid userId);

    public Task AddTaskAsync(TaskUser task, Guid userId);

    public Task DeleteTaskAsync(Guid taskId, Guid userId);

    public Task UpdateTaskAsync(TaskUser task);

    public Task AddCategoryAsync(Category category);

    public Task<Category?> GetCategoryByNameAsync(string name);

    public Task<List<Category>> GetUserCategoriesAsync(Guid userId);

    public Task<int> GetCountOfTasksAsync(string? search, string? category, Guid userId);
}
