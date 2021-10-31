using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

using CLup.Data;
using CLup.Data.Seed;

namespace CLup.Extensions
{
    public static class AppExtensions
    {

        public static async Task ConfigureDataInitialiser(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            CLupContext context = scope.ServiceProvider.GetRequiredService<CLupContext>();

            context.Database.EnsureCreated();
            var seeder = scope.ServiceProvider.GetRequiredService<Seeder>();

            await seeder.Seed();
        }
    }
}