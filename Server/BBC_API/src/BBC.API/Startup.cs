using System;
using System.Linq;
using Autofac;
using BBC.Core.IoC;
using BBC.Core.Kernel;
using BBC.Core.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BBC.Infrastructure;
using BBC.Services;
using Autofac.Extensions.DependencyInjection;

namespace BBC.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            new InfrastructureModule();
            new ServicesModule();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            KernelAssembly.SetAssembly(assemblies);
            ContainerBuilder builder = services.RegisterPopulate();
            builder.LoaderIoCManager();
            return new AutofacServiceProvider(IoCManager.Container);
        }

      
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
