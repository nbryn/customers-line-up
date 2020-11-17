using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

using Data;
using Logic.Businesses;
using Logic.BusinessQueues;
using Logic.Context;
using Logic.Users;
using Logic.Auth;
using Logic.Util;

namespace CLup
{
    public class Startup
    {

        internal static IConfiguration _config { get; private set; }
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CLupContext>(opt =>
            opt.UseInMemoryDatabase("CLup"));
            services.AddSingleton<IConfiguration>(_config);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.AddScoped<IDTOMapper, DTOMapper>();
            services.AddScoped<IBusinessQueueService, BusinessQueueService>();
            services.AddScoped<IBusinessQueueRepository, BusinessQueueRepository>();

            services.AddScoped<ICLupContext, CLupContext>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBusinessOwnerRepository, BusinessOwnerRepository>();
            services.AddScoped<IBusinessService, BusinessService>();
            services.AddScoped<IBusinessRepository, BusinessRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
