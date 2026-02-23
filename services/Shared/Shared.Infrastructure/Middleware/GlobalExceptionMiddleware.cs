using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shared.Application.Common.Exceptions;

namespace Shared.Infrastructure.Middleware;

public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

    private static bool IsDevelopment =>
        string.Equals(
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
            "Development",
            StringComparison.OrdinalIgnoreCase);

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            _logger.LogWarning(ex, "Application exception: {ErrorCode}", ex.ErrorCode);

            context.Response.StatusCode = (int)ex.StatusCode;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                StatusCode = ex.ErrorCode,
                Message = ex.Message
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {ExceptionType} — {Message}", ex.GetType().Name, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            // In Development: expose full detail so you can debug quickly.
            // In Production: return a safe generic message.
            var errorResponse = IsDevelopment
                ? new ErrorResponse
                {
                    StatusCode = "INTERNAL_SERVER_ERROR",
                    Message = ex.Message,
                    ExceptionType = ex.GetType().Name,
                    StackTrace = ex.StackTrace,
                    InnerException = ex.InnerException?.Message
                }
                : new ErrorResponse
                {
                    StatusCode = "INTERNAL_SERVER_ERROR",
                    Message = "An unexpected error occurred. Please try again later."
                };

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }

    private sealed class ErrorResponse
    {
        public string StatusCode { get; init; } = default!;
        public string Message { get; init; } = default!;

        // Only populated in Development
        public string? ExceptionType { get; init; }
        public string? StackTrace { get; init; }
        public string? InnerException { get; init; }
    }
}
