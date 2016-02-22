using System;
using Windows.ApplicationModel;
using Findier.Client.Windows.Extensions;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Client.Windows.Utilities
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

        public void Exit()
        {
            App.Current.Exit();
        }

        public void OnStart()
        {
            LaunchCount = _settingsUtility.Read("LaunchCount", 0) + 1;
            _settingsUtility.Write("LaunchCount", LaunchCount);

            // get current app version
            CurrentVersion = Package.Current.Id.Version.ToVersion();

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