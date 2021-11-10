using System.Threading.Tasks;
using CLup.Infrastructure;
using CLup.Infrastructure.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CLup.Application.Shared.Extensions
{
    public static class AppExtensions
    {
        
        public static async Task ConfigureSeed(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<CLupContext>();

            context.Database.EnsureCreated();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            await seeder.Seed();
        }
    }
}