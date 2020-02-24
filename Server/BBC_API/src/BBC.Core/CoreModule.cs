using Autofac;
using BBC.Core.Module;
using BBC.Core.Registery;

namespace BBC.Core
{
    public class CoreModule: BaseModule
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.DIRegister();
        }
    }
}
