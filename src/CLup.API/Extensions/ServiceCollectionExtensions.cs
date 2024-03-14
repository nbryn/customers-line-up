using System.Text;
using CLup.Application;
using CLup.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CLup.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerGenOptions =>
        {
            swaggerGenOptions.CustomSchemaIds(type => type.ToString());
            swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Customers Lineup Api", Version = "v1" });

            swaggerGenOptions.EnableAnnotations();
            swaggerGenOptions.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static IServiceCollection ConfigureJwt(this IServiceCollection services, AppSettings appSettings)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(config =>
        {
            config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            config.AddPolicy(Policies.User, Policies.UserPolicy());
        });

        return services;
    }

    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsApi", builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}
