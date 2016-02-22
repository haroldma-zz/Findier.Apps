using System;

namespace Findier.Core.Utilities.Interfaces
{
    public interface IApplicationUtility
    {
        Version CurrentVersion { get; }

        bool IsFirstLaunch { get; }

        bool JustUpdated { get; }

        int LaunchCount { get; }

        void Exit();

        void OnStart();
    }
}