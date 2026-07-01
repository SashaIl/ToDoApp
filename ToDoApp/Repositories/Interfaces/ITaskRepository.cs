using Microsoft.EntityFrameworkCore;
using ToDoApp.Db.Entities;

namespace ToDoApp.Repositories.Interfaces;

public interface ITaskRepository
{
    public Task<TaskUser?> GetTaskByIdAsync(Guid taskId, Guid userId);

    public List<TaskUser> GetAllUserTasks(Guid userId);

    public Task AddTaskAsync(TaskUser task, Guid userId);

    public Task DeleteTaskAsync(Guid taskId, Guid userId);

    public Task UpdateTaskAsync(TaskUser task);
}
