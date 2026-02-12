namespace Shared.Application.Common.Response;

public class BaseResponse<T>
{
    public bool Success { get; init; }
    public string? Message { get; init; }
    public string? ErrorCode { get; init; }
    public T? Data { get; init; }

    public static BaseResponse<T> Ok(T data, string? message = null) => new()
    {
        Success = true,
        Data = data,
        Message = message ?? "Success"
    };

    public static BaseResponse<T> Fail(string errorCode, string message) => new()
    {
        Success = false,
        ErrorCode = errorCode,
        Message = message
    };

    public static BaseResponse<T> Fail(string errorCode, string message, T? data) => new()
    {
        Success = false,
        ErrorCode = errorCode,
        Message = message,
        Data = data
    };
}
