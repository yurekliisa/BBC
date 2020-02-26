using Autofac;
using BBC.Core.Module;
using BBC.Core.Registery;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BBC.Core
{
    public class CoreModule: ModuleBase
    {
        public override void PreInit(IServiceCollection services, IConfiguration Configuration)
        {
            base.PreInit(services, Configuration);
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.DIRegister();
        }
        public override void PostInit(IApplicationBuilder app)
        {
            base.PostInit(app);
        }
    }
}
