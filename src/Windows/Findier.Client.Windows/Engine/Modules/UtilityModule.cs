using Autofac;
using Findier.Client.Windows.Utilities;
using Findier.Core.Utilities.DesignTime;
using Findier.Core.Utilities.Interfaces;
using Findier.Core.Utilities.RunTime;
using Findier.Core.Windows.Utilities;

namespace Findier.Client.Windows.Engine.Modules
{
    internal class UtilityModule : AppModule
    {
        public override void LoadDesignTime(ContainerBuilder builder)
        {
            builder.RegisterType<DesignDispatcherUtility>().As<IDispatcherUtility>();
            builder.RegisterType<DesignCredentialUtility>().As<ICredentialUtility>();
            builder.RegisterType<DesignSettingsUtility>().As<ISettingsUtility>();
            builder.RegisterType<DesignStorageUtility>().As<IStorageUtility>();
        }

        public override void LoadRunTime(ContainerBuilder builder)
        {
            builder.RegisterType<DispatcherUtility>().As<IDispatcherUtility>();
            builder.RegisterType<CredentialUtility>().As<ICredentialUtility>();
            builder.RegisterType<SettingsUtility>().As<ISettingsUtility>();
            builder.RegisterType<StorageUtility>().As<IStorageUtility>();
            builder.RegisterType<ApplicationUtility>().As<IApplicationUtility>().SingleInstance();
        }
    }
}