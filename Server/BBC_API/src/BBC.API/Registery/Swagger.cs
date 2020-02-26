using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API.Registery
{
    public static class Swagger
    {
        public static void UseSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogAPI", Version = "v1" });
            });
        }
        public static void SwaggerBuilder(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Blog API Version 1");
                options.DocumentTitle = "Title Documentation";
                options.DocExpansion(DocExpansion.None);
            });
        }
    }
}
