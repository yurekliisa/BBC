using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using BBC.Core.IoC;
using BBC.Core.Kernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Core.Module
{
    public static class Builder
    {
        public static ContainerBuilder RegisterPopulate(this IServiceCollection services)
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.Populate(services);

            var modular = KernelAssembly.GetAssemblyType()
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(BaseModule)) : false).Select(Activator.CreateInstance).Cast<BaseModule>().ToList();

            modular.ForEach(module =>
            {
                module.PreInit(services);
                builder.RegisterModule(module);
            });

            return builder;
        }

        public static void LoaderIoCManager(this ContainerBuilder builder)
        {
            IoCManager.Container = builder.Build();

            var modular = KernelAssembly.GetAssemblyType()
                .Where(module => module.BaseType != null ? module.BaseType.Equals(typeof(BaseModule)) : false).Select(Activator.CreateInstance).Cast<BaseModule>().ToList();

            modular.ForEach(module =>
            {
                module.PostInit(builder);
            });

        }
    }
}
