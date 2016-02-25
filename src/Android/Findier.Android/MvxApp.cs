using Android.Preferences;
using Findier.Android.ViewModels;
using Findier.Core.Android.Utilities;
using Findier.Core.Utilities.Interfaces;
using Findier.Core.Utilities.RunTime;
using Findier.Web.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace Findier.Android
{
    public class MvxApp : MvxApplication
    {
        public MvxApp()
        {
            Mvx.RegisterType<IDispatcherUtility>(() => new DispatcherUtility(() => App.Current.CurrentActivity));
            Mvx.RegisterType<ICredentialUtility, CredentialUtility>();
            Mvx.RegisterType<ISettingsUtility>(
                () => new SettingsUtility(PreferenceManager.GetDefaultSharedPreferences(App.Current)));
            Mvx.RegisterType<IStorageUtility, StorageUtility>();
            Mvx.ConstructAndRegisterSingleton<IApplicationUtility, ApplicationUtility>();
            Mvx.RegisterSingleton<IMvxAppStart>(new MvxAppStart<MainViewModel>());
            Mvx.RegisterType<IFindierService, FindierService>();
        }
    }
}