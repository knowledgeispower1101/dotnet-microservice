using Microsoft.Extensions.DependencyInjection;
using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Infrastructure.Repositories;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Application.Common.Interfaces.Security;
using ShoppeeClone.Infrastructure.Security;

namespace ShoppeeClone.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection infrastructures, IConfiguration configuration)
    {
        infrastructures.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        infrastructures.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        infrastructures.AddScoped<IUserRepository, UserRepo>();
        infrastructures.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        infrastructures.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention();
        });
        return infrastructures;
    }
}