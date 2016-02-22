using Autofac;
using Findier.Client.Windows.Engine.Navigation;
using Findier.Client.Windows.Services;
using Findier.Web.Services;

namespace Findier.Client.Windows.Engine.Modules
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
            builder.RegisterType<InsightsService>().As<IInsightsService>();
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<FindierService>().As<IFindierService>();
        }
    }
}