using CLup.API.Extensions;
using CLup.Application;
using CLup.Application.Auth;
using CLup.Domain;
using CLup.Infrastructure;
using CLup.WebUI.Extensions;
using FluentValidation.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CLup.API;

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
        services.ConfigureSwagger();

        services.AddAuthorization(config =>
        {
            config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            config.AddPolicy(Policies.User, Policies.UserPolicy());
        });

        services.AddControllers()
            .AddFluentValidation(fv =>
            {
                fv.ImplicitlyValidateChildProperties = true;
            });

        services.ConfigureDomain(_config);
        services.ConfigureApplication(_config);
        services.ConfigureInfrastructure(_config, Environment);
    }

    public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        await app.ConfigureSeed();
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
