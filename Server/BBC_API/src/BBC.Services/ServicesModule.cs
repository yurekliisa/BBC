using Autofac;
using BBC.Core.IoC;
using BBC.Core.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Services
{
    public class ServicesModule: BaseModule
    {
        public ServicesModule()
        {

        }
        public override void PreInit(IServiceCollection services, IConfiguration Configuration)
        {
            base.PreInit(services, Configuration);
        }
        public override void PostInit(IServiceProvider provider)
        {
            base.PostInit(provider);
        }
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
    }
}
