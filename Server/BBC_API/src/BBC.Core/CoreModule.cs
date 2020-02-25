using Autofac;
using BBC.Core.Module;
using BBC.Core.Registery;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BBC.Core
{
    public class CoreModule: BaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.DIRegister();
        }
        public override void PreInit(IServiceCollection services,IConfiguration Configuration)
        {
            base.PreInit(services,Configuration);
        }
        public override void PostInit(IServiceProvider provider)
        {
            base.PostInit(provider);
        }
        
    }
}
