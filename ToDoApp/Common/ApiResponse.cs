namespace ToDoApp.Common;

public class ApiResponse<T>
{

    public bool IsSuccess;
    public string? Message { get; set; }
    public T? Result { get; set; }

    public ApiResponse(bool isSuccess, string? message, T? result)
    {
        IsSuccess = isSuccess;
        Message = message;
        Result = result;
    }

    public static ApiResponse<T> Success(T? result)
        => new ApiResponse<T>(isSuccess: true, message: default, result: result);

    public static ApiResponse<T> Fail(string message)
        => new ApiResponse<T>(isSuccess: false, message: message, result: default);
}
