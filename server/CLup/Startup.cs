using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

using CLup.Auth;
using CLup.Bookings;
using CLup.Extensions;
namespace CLup
{
    public class Startup
    {
        internal IConfiguration _config { get; private set; }
        public IWebHostEnvironment Environment { get; }
        readonly string CorsApi = "CorsApi";
        public Startup(IConfiguration config, IWebHostEnvironment environment)
        {
            _config = config;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRouting(options => options.LowercaseUrls = true);
            services.ConfigureCors(_config);

            services.AddSingleton<IConfiguration>(_config);
            services.ConfigureJwt(_config);
            services.AddMediatR(typeof(Startup));

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.ConfigureServices(_config);
            services.ConfigureDataContext(_config, Environment);

            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger();
            services.AddControllers()
                    .AddFluentValidation(fv =>
                    {
                        fv.ImplicitlyValidateChildProperties = true;
                        fv.RegisterValidatorsFromAssemblyContaining<BookingValidator>();
                    });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureDataInitialiser();
            app.UseHttpsRedirection();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customers Lineup API");
                c.RoutePrefix = string.Empty;
                c.DocExpansion(DocExpansion.None);
                c.DisplayRequestDuration();
            });

            app.UseRouting();
            app.UseCors("CorsApi");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
