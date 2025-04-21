using BlackBox.Auth.Application.Common.Interface;
using BlackBox.Auth.Domain.Repository.Command.Base;
using BlackBox.Auth.Domain.Repository.Query.Base;
using BlackBox.Auth.Infrastructure.Data;
using BlackBox.Auth.Infrastructure.Identity;
using BlackBox.Auth.Infrastructure.Repository.Command.Base;
using BlackBox.Auth.Infrastructure.Repository.Query.Base;
using BlackBox.Auth.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackBox.Auth.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                ;
            services.Configure<IdentityOptions>(opt =>
            {
                // Default Lockout Setting
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;

                // Default SignIn Setting 
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequiredUniqueChars = 1;
                // Default SignIn settings.
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;
                opt.User.RequireUniqueEmail = true;
            });
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

            return services;
        }
    }
}
