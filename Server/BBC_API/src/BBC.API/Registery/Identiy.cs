using BBC.Core.Configuration.Config;
using BBC.Core.Domain.Identity;
using BBC.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API.Registery
{
    public static class Identiy
    {
        public static void UseIdentity<TContext, TUser, TRole>(this IServiceCollection services)
            where TContext : DbContext, IContext
            where TUser : class
            where TRole : class
        {
            services.AddIdentity<TUser, TRole>().AddEntityFrameworkStores<TContext>().AddDefaultTokenProviders().AddTokenProvider("BBC",typeof(DataProtectorTokenProvider<User>));
        }

        public static void SetIdentityOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<IdentityOptions>(
                options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 6;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = true;

                    // User settings
                    options.User.RequireUniqueEmail = true;

                    
                    //options.Password.RequireDigit = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":RequireDigit"]);
                    //options.Password.RequiredLength = Convert.ToInt32(Configuration[ConfigurationKeys.Identity+":RequireLength"]);
                    //options.Password.RequireNonAlphanumeric = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":RequireNonAlphanumeric"]);
                    //options.Password.RequireNonAlphanumeric = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":RequireNonAlphanumeric"]);
                    //options.Password.RequireUppercase = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":RequireUppercase"]);
                    //options.Password.RequireLowercase = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":RequireLowercase"]);
                    //options.Password.RequiredUniqueChars = Convert.ToInt32(Configuration[ConfigurationKeys.Identity+":RequiredUniqueChars"]);

                    //// Lockout settings
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(Convert.ToInt32(Configuration[ConfigurationKeys.Identity+":DefaultLockoutTimeSpan"]));
                    //options.Lockout.MaxFailedAccessAttempts = Convert.ToInt32(Configuration[ConfigurationKeys.Identity+":MaxFailedAccessAttempts"]); ;
                    //options.Lockout.AllowedForNewUsers = Convert.ToBoolean(Configuration[ConfigurationKeys.Identity+":AllowedForNewUsers"]);

                    //// User settings
                    //options.User.RequireUniqueEmail = Convert.ToBoolean(Configuration[":RequireUniqueEmail"]);

                });
        }
    }
}
