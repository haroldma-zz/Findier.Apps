using System;
using Android.App;
using Android.OS;
using Android.Runtime;

namespace Findier.Android
{
    [Application]
    public sealed class App : Application, Application.IActivityLifecycleCallbacks
    {
        public App(IntPtr a, JniHandleOwnership b) : base(a, b)
        {
            RegisterActivityLifecycleCallbacks(this);
        }

        public static App Current { get; private set; }

        public Activity CurrentActivity { get; private set; }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CurrentActivity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
            if (CurrentActivity != null && CurrentActivity.Equals(activity))
            {
                CurrentActivity = null;
            }
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Current = this;
        }
    }
}