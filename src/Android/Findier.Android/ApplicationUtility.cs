using System;
using Android.App;
using Findier.Core.Utilities.Interfaces;
using Java.Lang;

namespace Findier.Android
{
    internal class ApplicationUtility : IApplicationUtility
    {
        private readonly ISettingsUtility _settingsUtility;

        public ApplicationUtility(ISettingsUtility settingsUtility)
        {
            _settingsUtility = settingsUtility;
        }

        public Version CurrentVersion { get; private set; }

        public bool IsFirstLaunch { get; private set; }

        public bool JustUpdated { get; private set; }

        public int LaunchCount { get; private set; }

        public void OnStart()
        {
            LaunchCount = _settingsUtility.Read("LaunchCount", 0) + 1;
            _settingsUtility.Write("LaunchCount", LaunchCount);

            var versionName = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;
            if (versionName.Contains("."))
            {
                
            }
            // get current app version
            //CurrentVersion = Package.Current.Id.Version.ToVersion();

            // get previous app version
            var version = _settingsUtility.Read("LastLaunchVersion", CurrentVersion);

            // update the previous version with the current
            _settingsUtility.Write("LastLaunchVersion", CurrentVersion);

            // Check if we were just updated
            JustUpdated = CurrentVersion > version;

            IsFirstLaunch = LaunchCount == 1;
        }
    }
}