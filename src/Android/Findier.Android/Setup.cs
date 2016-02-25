using Android.Content;
using Findier.Android.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;

namespace Findier.Android
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new MvxApp();
        }
    }
}