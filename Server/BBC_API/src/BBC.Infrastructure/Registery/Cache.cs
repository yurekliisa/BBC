using Autofac;
using BBC.Core.Interfaces;
using BBC.Core.Kernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Infrastructure.Registery
{
    public static class Cache
    {
        public static void UseCache(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddMemoryCache(opt =>
            {
                opt.SizeLimit = Convert.ToInt64(Configuration["Cache:SizeLimit"]);
            });
        }
        public static void RegisterCache(this ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(KernelAssembly.GetAssemblies().ToArray())
                 .Where(type =>
                         type.GetInterfaces().Contains(typeof(ICacheManager))
                         )
                 .AsImplementedInterfaces()
                 .InstancePerLifetimeScope();
        }
    }
}
