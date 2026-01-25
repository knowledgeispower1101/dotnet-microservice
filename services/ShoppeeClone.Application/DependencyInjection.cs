using Microsoft.Extensions.DependencyInjection;
using ShoppeeClone.Application.Services.Authentication;

namespace ShoppeeClone.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}