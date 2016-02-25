using Android.App;
using Findier.Android.ViewModels;
using MvvmCross.Droid.Views;

namespace Findier.Android
{
    [Activity(Label = "Findier.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : MvxActivity<MainViewModel>
    {
        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            SetContentView(Resource.Layout.Main);
        }
    }
}