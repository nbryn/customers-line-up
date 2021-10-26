using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using CLup.Data;
using CLup.Data.Initializer;
using CLup.Features.Auth;
using CLup.Features.Users;

namespace CLup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CLupContext, CLupContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
        }
        
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Customers Lineup Api", Version = "v1" });

                c.EnableAnnotations();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                    },
                        new List<string>()
                    }
                });
            });
        }

        public static void ConfigureDataContext(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddTransient<DataInitializer>();

            if (environment.IsDevelopment())
            {
                var connectionString = configuration.GetConnectionString("development");

                services.AddDbContext<CLupContext>(options =>
                                  options.UseSqlServer(connectionString),
                       ServiceLifetime.Transient);
            }
            else
            {
                var connectionString = configuration.GetConnectionString("localdb");
                var normalizedConnString = NormalizeConnString(connectionString);
                services.AddDbContext<CLupContext>(options =>
                                  options.UseMySQL(normalizedConnString)                             
                                        .LogTo(Console.WriteLine, LogLevel.Information)
                                        .EnableSensitiveDataLogging()
                                        .EnableDetailedErrors()
                                  , ServiceLifetime.Transient);


                string NormalizeConnString(string raw)
                {
                    string conn = string.Empty;
                    try
                    {
                        var dict =
                             raw.Split(';')
                                 .Where(kvp => kvp.Contains('='))
                                 .Select(kvp => kvp.Split(new char[] { '=' }, 2))
                                 .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim(), StringComparer.InvariantCultureIgnoreCase);
                        var ds = dict["Data Source"];
                        var dsa = ds.Split(":");
                        //conn = $"server=127.0.0.1;userid=azure;password=;{dict["Password"]};database=localdb;Port={dsa[1]}";
                        conn = $"Server={dsa[0]};Port={dsa[1]};Database={dict["Database"]};Uid={dict["User Id"]};Pwd={dict["Password"]};";
                    }
                    catch
                    {
                        throw new Exception("Unexpected connection string: datasource is empty or null");
                    }
                    
                    return conn;
                }
            }
        }

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
        }

        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
                             {
                                 options.AddPolicy("CorsApi",
                                                     builder =>
                                                     {
                                                         builder.WithOrigins("http://localhost:3000")
                                                             .AllowAnyMethod()
                                                             .AllowAnyHeader()
                                                             .AllowCredentials();
                                                     });
                             });
        }
    }
}