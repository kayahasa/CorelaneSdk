namespace CorelaneSdk.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
    public string ErrorCode { get; set; } = string.Empty;
    public int StatusCode { get; set; }

    public static ApiResponse<Type> Instance<Type>(bool success, string message, Type data, List<string>? errors, string errorCode, int statusCode)
    {
        return new ApiResponse<Type>() 
        {
            Success = success,
            Message = message,
            Data = data,
            Errors = errors,
            ErrorCode = errorCode,
            StatusCode = statusCode,
        };
    }
}
