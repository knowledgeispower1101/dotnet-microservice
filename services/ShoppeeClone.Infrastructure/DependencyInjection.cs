using Microsoft.Extensions.DependencyInjection;
using ShoppeeClone.Application.Services.Authentication;

namespace ShoppeeClone.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection infrastructures)
    {
        return infrastructures;
    }
}