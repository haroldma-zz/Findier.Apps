using System;
using Windows.ApplicationModel;

namespace Findier.Windows.Extensions
{
    public static class PackageVersionExtensions
    {
        public static Version ToVersion(this PackageVersion packageVersion)
        {
            return new Version(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        }
    }
}