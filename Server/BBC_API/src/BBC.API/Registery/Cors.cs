using BBC.Core.Configuration.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API.Registery
{
    public static class Cors
    {
        private static string uiURL = string.Empty;
        public static void UseCors(this IServiceCollection services, IConfiguration Configuration)
        {
            uiURL = Configuration[ConfigurationKeys.Cors + ":UiUrl"].ToString();
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("AllowConfig",
            //        builder => builder
            //                        .WithOrigins(uiURL)
            //                        .SetIsOriginAllowedToAllowWildcardSubdomains()
            //                        .AllowAnyHeader()
            //                        .AllowAnyMethod()
            //                        .AllowCredentials()
            //            );
            //});
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }


        public static void CorsBuilder(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            //app.UseCors(x => x.WithOrigins(uiURL).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
        }
    }
}
