using Autofac;
using BBC.Core.Kernel;
using BBC.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BBC.Infrastructure.Registery
{
    public static class Database
    {
        public static void RegisterDatabase(this ContainerBuilder builder)
        {
            //var bb = KernelAssembly.GetAssemblies().ToArray().SelectMany(x => x.GetTypes()).Where(x => x.GetInterfaces().Contains(typeof(IBBCContext))).ToList();
            //foreach (var b in bb)
            //{
            //    builder.Register(c => b).AsImplementedInterfaces().InstancePerLifetimeScope();
            //}

            builder.RegisterAssemblyTypes(KernelAssembly.GetAssemblies().ToArray())
                 .Where(type =>
                         type.GetInterfaces().Contains(typeof(DbContext))
                         ).AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
