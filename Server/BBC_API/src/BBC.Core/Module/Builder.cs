using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using BBC.Core.IoC;
using BBC.Core.Kernel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Core.Module
{
    public static class Builder
    {
        public static void PreBuild(this IServiceCollection services, IConfiguration Configuration)
        {
            var modular = KernelAssembly.GetAssemblyType()
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(BaseModule)) : false).Select(Activator.CreateInstance).Cast<BaseModule>().ToList();

            modular.ForEach(module =>
            {
                module.PreInit(services,Configuration);
            });

           
        }

        public static void PostBuilder(this IServiceProvider provider)
        {
            var modular = KernelAssembly.GetAssemblyType()
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(BaseModule)) : false).Select(Activator.CreateInstance).Cast<BaseModule>().ToList();

            modular.ForEach(module =>
            {
                module.PostInit(provider);
            });

        }
    }
}
