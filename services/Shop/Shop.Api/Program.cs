using Microsoft.AspNetCore.Authentication.JwtBearer;
using Shared.Infrastructure.Middleware;
using Shop.Application;
using Shop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("CorsPolicy");

app.MapControllers();
app.Run();
