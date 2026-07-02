using Azure;
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
    public async Task<IActionResult> GetAllTasks(int pageSize, int page, string? search, string? category)
    {
        ApiResponse<List<MainTaskDto>> response = await _taskService.GetAllUserTasksAsync(pageSize, page, search, category);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [Authorize]
    [HttpPut("update_task/{taskId}")]
    public async Task<IActionResult> UpdateTask(Guid taskId, UpdateTaskDto updateTaskDto)
    {
        ApiResponse<object> response = await _taskService.UpdateTaskAsync(taskId, updateTaskDto);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [Authorize]
    [HttpDelete("delete_task/{taskId}")]
    public async Task<IActionResult> DeleteTask(Guid taskId)
    {
        ApiResponse<object> response = await _taskService.DeleteTaskAsync(taskId);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }

    [Authorize]
    [HttpPost("add_category/")]
    public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto)
    {
        ApiResponse<object> response = await _taskService.AddCategoryAsync(addCategoryDto);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [Authorize]
    [HttpGet("get_user_categories/")]
    public async Task<IActionResult> GetUserCategories()
    {
        ApiResponse<List<string>> response = await _taskService.GetUserCategoriesAsync();
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }


    [Authorize]
    [HttpGet("get_total_count_of_task/")]
    public async Task<IActionResult> GetountOfTasks(string? search, string? category)
    {
        ApiResponse<int> response = await _taskService.GetCountOfTasksAsync(search, category);
        return response.IsSuccess ? Ok(response) : BadRequest(response);
    }
}
