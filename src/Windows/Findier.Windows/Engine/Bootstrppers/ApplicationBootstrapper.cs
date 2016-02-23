using Autofac;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Windows.Engine.Bootstrppers
{
    internal class ApplicationBootstrapper : AppBootStrapper
    {
        public override void OnLaunched(IComponentContext context)
        {
            var applicationUtility = context.Resolve<IApplicationUtility>();
            applicationUtility.OnStart();
        }
    }
}