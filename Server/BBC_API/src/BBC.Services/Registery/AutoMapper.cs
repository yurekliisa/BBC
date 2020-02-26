using Autofac;
using AutoMapper;
using BBC.Services.Mapper.Config;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services.Registery
{
    public static class AutoMapper
    {
        public static void RegisterAutoMapper(this ContainerBuilder builder)
        {
            builder.Register(ctx =>
            {
                ILifetimeScope scope = ctx.Resolve<ILifetimeScope>();
                return AutoMapperConfiguration.Configure();
            }).As<IMapper>().SingleInstance();
        }
    }
}
