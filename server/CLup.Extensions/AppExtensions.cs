using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CLup.Context;
using CLup.Context.Initialiser;

namespace CLup.Extensions
{
    public static class AppExtensions
    {

        public static void ConfigureDataInitialiser(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            CLupContext context = scope.ServiceProvider.GetRequiredService<CLupContext>();

            context.Database.Migrate();
            
            var dataInitializer = scope.ServiceProvider.GetService<DataInitialiser>();

            dataInitializer.InitialiseSeed();

        }
    }
}