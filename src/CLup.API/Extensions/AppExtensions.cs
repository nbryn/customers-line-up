using CLup.Infrastructure.Persistence;
using CLup.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;

namespace CLup.API.Extensions;

public static class AppExtensions
{
    public static async Task ConfigureSeed(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CLupDbContext>();

        await context.Database.MigrateAsync();
        var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();

        await seeder.Seed();
    }
}
