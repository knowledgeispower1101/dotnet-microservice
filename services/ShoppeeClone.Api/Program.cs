using ShoppeeClone.Api.Middleware;
using ShoppeeClone.Application;
using ShoppeeClone.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddControllers();
}


var app = builder.Build();
{
    app.UseMiddleware<GlobalExceptionMiddleware>();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}

