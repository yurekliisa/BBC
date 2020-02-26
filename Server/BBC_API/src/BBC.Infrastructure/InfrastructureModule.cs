using Autofac;
using BBC.Core.Configuration;
using BBC.Core.Domain.Helper;
using BBC.Core.IoC;
using BBC.Core.Module;
using BBC.Core.Registery;
using BBC.Infrastructure.Data;
using BBC.Infrastructure.Registery;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BBC.Infrastructure
{
    public class InfrastructureModule : ModuleBase
    {
        public override void PreInit(IServiceCollection services, IConfiguration Configuration)
        {
            services.UseCache(Configuration);
            services.UseSQL(Configuration);
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterCache();
            builder.RegisterDatabase();
            builder.RegisterGenericRepository();
            builder.RegisterRepository();
        }
        public override void PostInit(IApplicationBuilder app)
        {
            app.RegisterEntities();
        }

    }
}
