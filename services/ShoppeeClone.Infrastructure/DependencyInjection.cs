using Microsoft.Extensions.DependencyInjection;
using ShoppeeClone.Application.Common.Interfaces.Authentication;
using ShoppeeClone.Application.Services.Authentication;
using ShoppeeClone.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;

namespace ShoppeeClone.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection infrastructures, IConfiguration configuration)
    {
        infrastructures.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        infrastructures.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        return infrastructures;
    }
}