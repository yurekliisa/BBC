using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using BBC.Core.IoC;
using BBC.Core.Kernel;
using Microsoft.AspNetCore.Builder;
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
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(ModuleBase)) : false).Select(Activator.CreateInstance).Cast<ModuleBase>().ToList();

            modular.ForEach(module =>
            {
                module.PreInit(services,Configuration);
            });

           
        }

        public static void PostBuilder(this IApplicationBuilder app)
        {
            var modular = KernelAssembly.GetAssemblyType()
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(ModuleBase)) : false).Select(Activator.CreateInstance).Cast<ModuleBase>().ToList();

            modular.ForEach(module =>
            {
                module.PostInit(app);
            });

        }
    }
}
