using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CLup.Domain;

public static class DomainConfiguration
{
    public static IServiceCollection ConfigureDomain(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
