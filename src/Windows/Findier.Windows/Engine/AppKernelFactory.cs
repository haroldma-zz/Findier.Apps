using Autofac;
using Findier.Windows.Engine.Bootstrppers;
using Findier.Windows.Engine.Modules;

namespace Findier.Windows.Engine
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