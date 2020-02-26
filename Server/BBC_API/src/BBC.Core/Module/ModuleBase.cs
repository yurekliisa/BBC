using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BBC.Core.Module
{
    public class ModuleBase : Autofac.Module
    {
        protected ModuleBase()
        {
            //KernelAssembly.SetAssembly(GetType().Assembly);
        }

        public virtual void PreInit(IServiceCollection services,IConfiguration Configuration)
        {
          
        }

        public virtual void PostInit(IApplicationBuilder app)
        {

        }
    }
}
