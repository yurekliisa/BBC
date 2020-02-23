using Autofac;
using BBC.Core;
using BBC.Core.Module;
using BBC.Infrastructure;
using BBC.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBC.API
{
    public class APIModule: BaseModule
    {
     
        protected override void Load(ContainerBuilder builder)
        {
           
        }

        public override void PreInit(IServiceCollection services)
        {
           
        }
    }
}
