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
using BBC.Core;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Collections.Generic;
using BBC.API.Middlewares;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BBC.API
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //services.AddControllers();
        
            var assemblies = new List<Assembly>();
            Assembly current = this.GetType().Assembly;
            assemblies.Add(current);

            var references = current.GetReferencedAssemblies().Where(x => x.Name.Contains("BBC"));
            
            foreach (var reference in references)
            {
                assemblies.Add(Assembly.Load(reference));
            }

            KernelAssembly.SetAssembly(assemblies);
            services.PreBuild(Configuration);

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ServicesModule());
            builder.RegisterModule(new APIModule());

        }
        public void Configure(
          IApplicationBuilder app,
          IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Upload")),
                RequestPath = new PathString("/Upload")
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Upload")),
                RequestPath = new PathString("/Upload")
            });

            IoCManager.Container = app.ApplicationServices.GetAutofacRoot();

            app.UseHttpsRedirection();

            app.UseRouting();


            app.PostBuilder();

            app.UseErrorHandlingMiddleware();

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            


        }
    }
}
