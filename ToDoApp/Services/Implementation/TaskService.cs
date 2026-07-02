using AutoMapper;
using System.Threading.Tasks;
using ToDoApp.Common;
using ToDoApp.Common.Dtos;
using ToDoApp.Db.Entities;
using ToDoApp.Repositories.Interfaces;
using ToDoApp.Services.Interfaces;

namespace ToDoApp.Services.Implementation;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper, IJwtService jwtService)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _jwtService = jwtService;
    }


    public async Task<ApiResponse<List<MainTaskDto>>> GetAllUserTasksAsync(int pageSize, int page, string? search, string? category)
    {

        try
        {
            Guid userId = _jwtService.GetCurrentUserId();

            bool isUserExists = await _userRepository.GetUserByIdAsync(userId) != null;
            if (!isUserExists)
            {
                return ApiResponse<List<MainTaskDto>>.Fail("User not found");
            }

            List<TaskUser> tasks = await _taskRepository.GetAllUserTasksAsync(pageSize, page, search, category!, userId);
            List<MainTaskDto> taskDtos = _mapper.Map<List<MainTaskDto>>(tasks);

            return ApiResponse<List<MainTaskDto>>.Success(taskDtos);
        }
        catch(Exception ex)
        {
            return ApiResponse<List<MainTaskDto>>.Fail(ex.Message);
        }   
    }


    public async Task<ApiResponse<object>> AddTaskAsync(AddTaskDto addTaskDto)
    {
        try
        {
            Guid userId = _jwtService.GetCurrentUserId();


            Category? category = await _taskRepository.GetCategoryByNameAsync(addTaskDto.Category);
            bool isCategoryExists = category != null;
            if (!isCategoryExists)
            {
                return ApiResponse<object>.Fail("Category not found");
            }


            bool isUserExists = await _userRepository.GetUserByIdAsync(userId) != null;
            if (!isUserExists)
            {
                return ApiResponse<object>.Fail("User not found");
            }

            TaskUser task = new TaskUser()
            {
                Name = addTaskDto.Name.Trim(),
                Description = addTaskDto.Description.Trim(),
                Category = category!,
                UserId = userId
            };

            await _taskRepository.AddTaskAsync(task, userId);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }


    public async Task<ApiResponse<object>> DeleteTaskAsync(Guid taskId)
    {
        try
        {
            Guid userId = _jwtService.GetCurrentUserId();   

            TaskUser? task = await _taskRepository.GetTaskByIdAsync(taskId, userId);
            if(task is null)
            {
                return ApiResponse<object>.Fail("Task not found");
            }

            await _taskRepository.DeleteTaskAsync(taskId, userId);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<object>> UpdateTaskAsync(Guid taskId, UpdateTaskDto updateTaskDto)
    {
        try
        {
            Guid userId = _jwtService.GetCurrentUserId();

            TaskUser? task = await _taskRepository.GetTaskByIdAsync(taskId, userId);
            if (task is null)
            {
                return ApiResponse<object>.Fail("Task not found");
            }

            Category? category = await _taskRepository.GetCategoryByNameAsync(updateTaskDto.Category);
            bool isCategoryExists = category != null;
            if (!isCategoryExists)
            {
                return ApiResponse<object>.Fail("Category not found");
            }


            task.Category = category!;
            task.Name = updateTaskDto.Name.Trim();
            task.Description = updateTaskDto.Description.Trim();

            await _taskRepository.UpdateTaskAsync(task);

            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }


    public async Task<ApiResponse<object>> AddCategoryAsync(AddCategoryDto addCategoryDto)
    {
        try
        {

            Guid userId = _jwtService.GetCurrentUserId();

            if(addCategoryDto.Name.Length < 3)
            {
                return ApiResponse<object>.Fail("Category name must be at least 3 characters long");
            }


            Category category = new Category()
            {
                UserId = userId,
                Name = addCategoryDto.Name
            };

            await _taskRepository.AddCategoryAsync(category);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);    
        }
    }


    public async Task<ApiResponse<List<string>>> GetUserCategoriesAsync()
    {
        try
        {
            Guid userId = _jwtService.GetCurrentUserId();
            List<Category> categories = await _taskRepository.GetUserCategoriesAsync(userId);

            List<string> categoriesStr = categories.Select(c => c.Name).ToList();

            return ApiResponse<List<string>>.Success(categoriesStr);
        }
        catch (Exception ex)
        {
            return ApiResponse<List<string>>.Fail(ex.Message);
        }
    }


    public async Task<ApiResponse<int>> GetCountOfTasksAsync(string? search, string? category)
    {

        try
        {
            Guid userId = _jwtService.GetCurrentUserId();
            bool isUserExist = await _userRepository.GetUserByIdAsync(userId) != null;
            if (!isUserExist)
            {
                return ApiResponse<int>.Fail("User not found");
            }

            int count = await _taskRepository.GetCountOfTasksAsync(search, category, userId);
            return ApiResponse<int>.Success(count);
        }
        catch(Exception ex)
        {
            return ApiResponse<int>.Fail(ex.Message);
        }
    }
}


