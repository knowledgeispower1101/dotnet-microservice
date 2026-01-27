
using ShoppeeClone.Application.Common.Exceptions;

namespace ShoppeeClone.Api.Middleware;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private static readonly Dictionary<Type, int> ExceptionStatusMap = new()
{
    { typeof(UserAlreadyExistsException), StatusCodes.Status409Conflict }
};
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = ExceptionStatusMap.TryGetValue(ex.GetType(), out var statusCode) ? statusCode : StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = ex.ErrorCode,
                message = ex.Message
            });
        }
        catch (Exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                errorCode = "INTERNAL_SERVER_ERROR",
                message = "Something went wrong"
            });
        }
    }
}
