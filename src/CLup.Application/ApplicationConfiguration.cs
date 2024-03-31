using System.Reflection;
using CLup.Application.Auth;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CLup.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
