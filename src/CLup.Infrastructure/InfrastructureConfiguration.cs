using CLup.Application;
using CLup.Application.Shared.Interfaces;
using CLup.Infrastructure.Persistence;
using CLup.Infrastructure.Persistence.Seed;
using CLup.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CLup.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection ConfigureInfrastructure(
        this IServiceCollection services,
        AppSettings appSettings,
        IWebHostEnvironment environment)
    {
        services.AddScoped<ICLupRepository, CLupDbContext>();
        services.AddScoped<IDomainEventService, DomainEventService>();
        services.AddTransient<ISeeder, Seeder>();

        ConfigureDb(services, appSettings, environment);

        return services;
    }

    private static void ConfigureDb(
        IServiceCollection services,
        AppSettings appSettings,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddDbContext<CLupDbContext>(options =>
                    options.UseNpgsql(appSettings.ConnectionStrings.Development),
                ServiceLifetime.Transient);
        }
        else
        {
            services.AddDbContext<CLupDbContext>(options =>
                    options.UseSqlServer(appSettings.ConnectionStrings.Production, options => options.EnableRetryOnFailure(12)),
                ServiceLifetime.Transient);
        }
    }
}
