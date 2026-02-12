
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minio;
using StackExchange.Redis;
using User.Application.Authentication.Persistence;
using User.Application.Common.Behaviors;
using User.Application.Common.Interfaces;
using User.Application.Upload.Persistence;
using User.Infrastructure.Authentication.Jwt;
using User.Infrastructure.Authentication.Password;
using User.Infrastructure.Authentication.RefreshTokens;
using User.Infrastructure.Cache;
using User.Infrastructure.Repositories;
using User.Infrastructure.Storage;

namespace User.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .AddSettings(configuration)
            .AddRedis()
            .AddMinio()
            .AddDatabase(configuration)
            .AddRepositories()
            .AddAuth()
            .AddBehaviors();

        return services;
    }

    private static IServiceCollection AddSettings(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<RedisSettings>(configuration.GetSection("Redis"));
        services.Configure<MinioSettings>(configuration.GetSection("Minio"));
        return services;
    }

    private static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
            var options = ConfigurationOptions.Parse(settings.Connection);
            options.AbortOnConnectFail = false;
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();
        return services;
    }

    private static IServiceCollection AddMinio(this IServiceCollection services)
    {
        services.AddSingleton<IMinioClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

            return new MinioClient()
                .WithEndpoint(settings.EndPoint)
                .WithCredentials(settings.AccessKey, settings.SecretKey)
                .WithSSL(settings.UseSSL)
                .Build();
        });

        services.AddScoped<IObjectStorage, MinioObjectStorage>();
        return services;
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
                   .UseSnakeCaseNamingConvention());

        // services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepo>();
        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
        services.AddScoped<IRefreshTokens, CryptoRefreshTokenGenerator>();
        return services;
    }

    private static IServiceCollection AddBehaviors(this IServiceCollection services)
    {
        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(TransactionBehavior<,>)
        );
        return services;
    }
}
