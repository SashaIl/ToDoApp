using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [Authorize]
    [HttpPost("add_task/")]
    public async Task<IActionResult> AddTask(AddTaskDto addTaskDto)
    {
        ApiResponse<object> response = await _taskService.AddTaskAsync(addTaskDto);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [Authorize]
    [HttpGet("get_user_tasks/")]
    public async Task<IActionResult> GetAllTasks(Guid userId)
    {
        ApiResponse<List<TaskBaseDto>> tasks = await _taskService.GetAllUserTasksAsync(userId);
        return Ok(tasks);
    }


    [Authorize]
    [HttpPut("update_task/")]
    public async Task<IActionResult> UpdateTask(UpdateTaskDto updateTaskDto)
    {
        ApiResponse<object> response = await _taskService.UpdateTaskAsync(updateTaskDto);
        return Ok(response);
    }


    [Authorize]
    [HttpDelete("delete_task/")]
    public async Task<IActionResult> DeleteTask(DeleteTaskDto deleteTaskDto)
    {
        ApiResponse<object> response = await _taskService.DeleteTaskAsync(deleteTaskDto);
        return Ok(response);
    }

}
