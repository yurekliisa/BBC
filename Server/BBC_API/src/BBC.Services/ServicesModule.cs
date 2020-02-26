using Autofac;
using BBC.Core.IoC;
using BBC.Core.Module;
using BBC.Services.Registery;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services
{
    public class ServicesModule: ModuleBase
    {
        public override void PreInit(IServiceCollection services, IConfiguration Configuration)
        {
            base.PreInit(services, Configuration);
        }
        public override void PostInit(IApplicationBuilder app)
        {
            base.PostInit(app);
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
            builder.RegisterAutoMapper();
        }
    }
}
