using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

using CLup.Auth;
using CLup.Bookings;
using CLup.Bookings.Interfaces;
using CLup.Businesses;
using CLup.Businesses.Interfaces;
using CLup.Context;
using CLup.Context.Initialiser;
using CLup.Employees;
using CLup.Employees.Interfaces;
using CLup.TimeSlots;
using CLup.TimeSlots.Interfaces;
using CLup.Users;
using CLup.Users.Interfaces;

namespace CLup.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICLupContext, CLupContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<IBusinessOwnerRepository, BusinessOwnerRepository>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

        }

        public static void ConfigureDataContext(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddTransient<DataInitialiser>();

            if (environment.IsDevelopment())
            {
                var connectionString = configuration.GetConnectionString("development");

                services.AddDbContext<CLupContext>(options =>
                                  options.UseSqlServer(connectionString),
                       ServiceLifetime.Transient);
            }
            else
            {
                var connectionString = "DataSource=myshareddb;mode=memory;cache=shared";
                services.AddDbContext<CLupContext>(options =>
                                  options.UseSqlite(connectionString),
                       ServiceLifetime.Transient);
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