using System.Threading.Tasks;
using CLup.Data;
using CLup.Data.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CLup.Application.Extensions
{
    public static class AppExtensions
    {
        
        public static async Task ConfigureSeed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CLupContext>();

            await context.Database.EnsureCreatedAsync();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            await seeder.Seed();
        }
    }
}