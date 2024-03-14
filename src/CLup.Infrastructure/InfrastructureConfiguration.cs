using CLup.Application;
using CLup.Application.Shared.Interfaces;
using CLup.Infrastructure.Persistence;
using CLup.Infrastructure.Persistence.Seed;
using CLup.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
            var connectionString = appSettings.ConnectionStrings.Production;
            var normalizedConnString = NormalizeConnString(connectionString);
            services.AddDbContext<CLupDbContext>(options =>
                    options.UseMySQL(normalizedConnString)
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors()
                , ServiceLifetime.Transient);


            string NormalizeConnString(string raw)
            {
                string conn = string.Empty;
                try
                {
                    var dict =
                        raw.Split(';')
                            .Where(kvp => kvp.Contains('='))
                            .Select(kvp => kvp.Split(new char[] {'='}, 2))
                            .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim(),
                                StringComparer.InvariantCultureIgnoreCase);
                    var ds = dict["Data Source"];
                    var dsa = ds.Split(":");
                    //conn = $"server=127.0.0.1;userid=azure;password=;{dict["Password"]};database=localdb;Port={dsa[1]}";
                    conn =
                        $"Server={dsa[0]};Port={dsa[1]};Database={dict["Database"]};Uid={dict["User Id"]};Pwd={dict["Password"]};";
                }
                catch
                {
                    throw new Exception("Unexpected connection string: datasource is empty or null");
                }

                return conn;
            }
        }
    }
}
