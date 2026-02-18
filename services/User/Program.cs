using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Infrastructure.Cache;
using Shared.Infrastructure.Middleware;
using StackExchange.Redis;
using System.Text;
using User.Data;
using User.Interfaces;
using User.Services;
using User.Services.Authentication;
using User.Services.Jwt;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.Configure<JwtSettings>(
    configuration.GetSection("JWT_SETTINGS"));

builder.Services.Configure<RedisSettings>(
    configuration.GetSection("Redis"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        configuration.GetConnectionString("DefaultConnection"))
           .UseSnakeCaseNamingConvention());

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;

    if (string.IsNullOrEmpty(settings.Connection))
        throw new Exception("Redis connection string is missing!");

    var options = ConfigurationOptions.Parse(settings.Connection);
    options.AbortOnConnectFail = false;

    return ConnectionMultiplexer.Connect(options);
});

builder.Services.AddScoped<IRefreshTokenStore, RedisRefreshTokenStore>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<IRefreshTokens, CryptoRefreshTokenGenerator>();

builder.Services.AddCors(options =>
    options.AddPolicy("CorsPolicy", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()));

var jwtSettings = configuration
    .GetSection("JWT_SETTINGS")
    .Get<JwtSettings>();

if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.SecretKey))
    throw new Exception("JWT configuration is missing!");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
