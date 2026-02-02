namespace ShoppeeClone.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ShoppeeClone.Infrastructure.Repositories;
using ShoppeeClone.Application.Services.Persistence;
using ShoppeeClone.Infrastructure.Authentication.Jwt;
using ShoppeeClone.Infrastructure.Authentication.Password;
using ShoppeeClone.Infrastructure.Authentication.RefreshTokens;
using ShoppeeClone.Application.Common.Interfaces;
using ShoppeeClone.Infrastructure.Persistence;
using MediatR;
using ShoppeeClone.Application.Common.Behaviors;
using StackExchange.Redis;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        services.Configure<RedisSettings>(
            configuration.GetSection("Redis"));

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var redisSettings = sp
                .GetRequiredService<IOptions<RedisSettings>>()
                .Value;

            var options = ConfigurationOptions.Parse(redisSettings.Connection);
            options.AbortOnConnectFail = false;

            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IUserRepository, UserRepo>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IRefreshTokens, CryptoRefreshTokenGenerator>();
        services.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(TransactionBehavior<,>)
        );

        return services;
    }
}
