using Autofac;
using Findier.Client.Windows.Engine.Bootstrppers;
using Findier.Client.Windows.Engine.Modules;

namespace Findier.Client.Windows.Engine
{
    internal static class AppKernelFactory
    {
        public static AppKernel Create() => new AppKernel(GetModules(), GetBootStrappers());

        public static IBootStrapper[] GetBootStrappers() =>
            new IBootStrapper[]
            {
                new ApplicationBootstrapper()
            };

        public static Module[] GetModules() =>
            new Module[]
            {
                new UtilityModule(),
                new ServiceModule(),
                new ViewModelModule()
            };
    }
}