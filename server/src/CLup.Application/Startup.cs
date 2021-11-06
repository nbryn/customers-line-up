using CLup.Application.Auth;
using CLup.Application.Extensions;
using CLup.Domain.Users;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CLup.Application
{
    public class Startup
    {
        internal IConfiguration _config { get; private set; }
        public IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration config, IWebHostEnvironment environment)
        {
            _config = config;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddRouting(options => options.LowercaseUrls = true);
            services.ConfigureCors(_config);

            services.AddSingleton(_config);
            services.ConfigureJwt(_config);
            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(User));

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
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                        fv.RegisterValidatorsFromAssemblyContaining<User>();
                    });
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            await app.ConfigureSeeder();
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
