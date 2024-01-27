using CLup.Infrastructure.Persistence;
using CLup.Infrastructure.Persistence.Seed;

namespace CLup.API.Extensions;

public static class AppExtensions
{
    public static async Task ConfigureSeed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CLupDbContext>();

        context.Database.EnsureCreated();
        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

        await seeder.Seed();
    }
}
