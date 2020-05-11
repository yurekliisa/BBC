using Autofac;
using BBC.Core.Dependency;
using BBC.Core.Kernel;
using BBC.Core.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BBC.Core.Registery
{
    public static class Dependency
    {
        public static void DIRegister(this ContainerBuilder builder)
        {
            builder.DIRegisterTransient();
            builder.DIRegisterSingleton();
        }

        private static void DIRegisterTransient(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(KernelAssembly.GetAssemblies().ToArray())
               .Where(type =>
                       type.GetInterfaces().Contains(typeof(ITransientDI))
                       )
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }

        private static void DIRegisterSingleton(this ContainerBuilder builder)
        {
            builder.Register(c => new Configuration.ConfigDatabase()).SingleInstance();
            builder.Register(c => new Configuration.ConfigJWT()).SingleInstance();
            builder.Register(c => new Configuration.ConfigEmail()).SingleInstance();
            builder.Register(c => new Configuration.ConfigClientApp()).SingleInstance();
            builder.Register(c => new Configuration.ConfigQRCode()).SingleInstance();
            builder.Register(c => new Permissions()).SingleInstance();
            
            /*
             * // TODO : Not working
            builder.RegisterAssemblyTypes()
            .Where(type =>
                    type.GetInterfaces().Contains(typeof(ISingletonDI)) && type.IsClass
                    )
            .AsImplementedInterfaces()
            .SingleInstance();*/
        }
    }
}

