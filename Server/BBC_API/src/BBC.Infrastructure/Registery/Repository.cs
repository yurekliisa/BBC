using Autofac;
using BBC.Core.Interfaces;
using BBC.Core.Kernel;
using BBC.Core.Repositories.Base;
using BBC.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Infrastructure.Registery
{
    public static class Repository
    {
        public static void RegisterGenericRepository(this ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(RepositoryBase<,,>)).As(typeof(IRepositoryBase<,,>)).InstancePerLifetimeScope();
        }

        public static void RegisterRepository(this ContainerBuilder builder)
        {

            builder.RegisterAssemblyTypes(KernelAssembly.GetAssemblies().ToArray())
                .Where(type =>
                        type.GetInterfaces().Contains(typeof(IRepository))
                        )
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
