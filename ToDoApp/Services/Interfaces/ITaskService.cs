using System.Threading.Tasks;
using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;

namespace ToDoApp.Services.Interfaces;

public interface ITaskService
{
    public Task<ApiResponse<List<MainTaskDto>>> GetAllUserTasksAsync(int pageSize, int page, string? search, string? category);

    public Task<ApiResponse<object>> AddTaskAsync(AddTaskDto addTaskDto);

    public Task<ApiResponse<object>> DeleteTaskAsync(Guid taskId);

    public Task<ApiResponse<object>> UpdateTaskAsync(Guid taskId, UpdateTaskDto updateTaskDto);

    public Task<ApiResponse<object>> AddCategoryAsync(AddCategoryDto addCategoryDto);

    public Task<ApiResponse<List<string>>> GetUserCategoriesAsync();

    public Task<ApiResponse<int>> GetCountOfTasksAsync(string? search, string? category);
}

