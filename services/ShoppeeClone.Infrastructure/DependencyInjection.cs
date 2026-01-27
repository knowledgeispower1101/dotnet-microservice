namespace ShoppeeClone.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Infrastructure.Repositories;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Infrastructure.Authentication.Jwt;
using ShoppeeClone.Infrastructure.Authentication.Password;
using ShoppeeClone.Application.Common.Interfaces.Authentication.Password;
using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Infrastructure.Authentication.RefreshTokens;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection infrastructures, IConfiguration configuration)
    {
        infrastructures.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        infrastructures.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        infrastructures.AddScoped<IUserRepository, UserRepo>();
        infrastructures.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        infrastructures.AddScoped<IRefreshTokens, CryptoRefreshTokenGenerator>();
        infrastructures.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();
        infrastructures.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention();
        });
        return infrastructures;
    }
}