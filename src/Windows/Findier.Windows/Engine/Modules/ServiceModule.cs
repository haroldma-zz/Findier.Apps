using Autofac;
using Findier.Web.Services;
using Findier.Windows.Engine.Navigation;
using Findier.Windows.Services;

namespace Findier.Windows.Engine.Modules
{
    internal class ServiceModule : AppModule
    {
        public override void LoadDesignTime(ContainerBuilder builder)
        {
            builder.RegisterType<DesignInsightsService>().As<IInsightsService>();
            builder.RegisterType<DesignNavigationService>().As<INavigationService>();
            builder.RegisterType<DesignFindierService>().As<IFindierService>();
        }

        public override void LoadRunTime(ContainerBuilder builder)
        {
            builder.RegisterType<InsightsService>().As<IInsightsService>().SingleInstance();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<FindierService>().As<IFindierService>().SingleInstance();
        }
    }
}