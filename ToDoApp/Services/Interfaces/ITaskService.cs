using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;

namespace ToDoApp.Services.Interfaces;

public interface ITaskService
{
    public Task<ApiResponse<List<TaskBaseDto>>> GetAllUserTasksAsync(Guid userId);

    public Task<ApiResponse<object>> AddTaskAsync(AddTaskDto addTaskDto);

    public Task<ApiResponse<object>> DeleteTaskAsync(DeleteTaskDto deleteTaskDto);

    public Task<ApiResponse<object>> UpdateTaskAsync(UpdateTaskDto updateTaskDto);
}

