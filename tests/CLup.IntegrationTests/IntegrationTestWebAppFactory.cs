using CLup.API;
using CLup.Infrastructure.Persistence;
using CLup.Infrastructure.Persistence.Seed;
using DotNet.Testcontainers.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace tests.CLup.IntegrationTests;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("clup")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var seederDescriptor = services.SingleOrDefault(service => service.ServiceType == typeof(Seeder));
            var dbContextDescriptor =
                services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<CLupDbContext>));

            services.Remove(seederDescriptor);
            services.Remove(dbContextDescriptor);
            services.AddTransient<ISeeder, TestSeeder>();
            services.AddDbContext<CLupDbContext>(options =>
            {
                options.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }

    public Task InitializeAsync() => _dbContainer.StartAsync();

    public new Task DisposeAsync() => Task.FromResult(_dbContainer.DisposeAsync());
}
