using AutoMapper;
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

    private readonly IMapper _mapper;

    public TaskService(ITaskRepository taskRepository, IUserRepository userRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }


    public async Task<ApiResponse<List<TaskBaseDto>>> GetAllUserTasksAsync(Guid userId)
    {

        bool isUserExists = await _userRepository.GetUserByIdAsync(userId) != null;
        if (!isUserExists)
        {
            return ApiResponse<List<TaskBaseDto>>.Fail("User not found");
        }

        List<TaskUser> tasks = _taskRepository.GetAllUserTasks(userId);
        List<TaskBaseDto> taskDtos = _mapper.Map<List<TaskBaseDto>>(tasks);

        return ApiResponse<List<TaskBaseDto>>.Success(taskDtos);
    }


    public async Task<ApiResponse<object>> AddTaskAsync(AddTaskDto addTaskDto)
    {
        try
        {
            Guid userId = addTaskDto.UserId;
            
            bool isUserExists = await _userRepository.GetUserByIdAsync(userId) != null;
            if (!isUserExists)
            {
                return ApiResponse<object>.Fail("User not found");
            }

            TaskUser task = _mapper.Map<AddTaskDto, TaskUser>(addTaskDto);

            await _taskRepository.AddTaskAsync(task, userId);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }


    public async Task<ApiResponse<object>> DeleteTaskAsync(DeleteTaskDto deleteTaskDto)
    {
        try
        {
            TaskUser? task = await _taskRepository.GetTaskByIdAsync(deleteTaskDto.TaskId, deleteTaskDto.UserId);
            if(task is null)
            {
                return ApiResponse<object>.Fail("Task not found");
            }

            await _taskRepository.DeleteTaskAsync(deleteTaskDto.TaskId, deleteTaskDto.UserId);
            return ApiResponse<object>.Success(default);
        }
        catch(Exception ex)
        {
            return ApiResponse<object>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<object>> UpdateTaskAsync(UpdateTaskDto updateTaskDto)
    {
        try
        {
            TaskUser? task = await _taskRepository.GetTaskByIdAsync(updateTaskDto.TaskId, updateTaskDto.UserId);
            if (task is null)
            {
                return ApiResponse<object>.Fail("Task not found");
            }

            task.Category = updateTaskDto.Category;
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
}


