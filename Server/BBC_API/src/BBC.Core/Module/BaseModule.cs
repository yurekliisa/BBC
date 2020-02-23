using Autofac;
using BBC.Core.Kernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBC.Core.Module
{
    public class BaseModule : Autofac.Module
    {
        protected BaseModule()
        {
            KernelAssembly.SetAssembly(GetType().Assembly);
        }

        public virtual void PreInit(IServiceCollection services)
        {
          
        }
     
        public virtual void PostInit(ContainerBuilder builder)
        {

        }
    }
}
