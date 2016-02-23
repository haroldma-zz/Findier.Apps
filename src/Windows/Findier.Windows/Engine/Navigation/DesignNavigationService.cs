using System;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Windows.Engine.Navigation
{
    public class DesignNavigationService : INavigationService
    {
        public bool CanGoBack { get; }

        public bool CanGoForward { get; }

        public object CurrentPageParam { get; }

        public Type CurrentPageType { get; }

        public IDispatcherUtility Dispatcher { get; }

        public Frame Frame { get; }

        public FrameFacade FrameFacade { get; }

        public event TypedEventHandler<Type> AfterRestoreSavedNavigation;

        public void ClearCache(bool removeCachedPagesInBackStack = false)
        {
            throw new NotImplementedException();
        }

        public void ClearHistory()
        {
            throw new NotImplementedException();
        }

        public void GoBack()
        {
            throw new NotImplementedException();
        }

        public void GoForward()
        {
            throw new NotImplementedException();
        }

        public bool Navigate(Type page, object parameter = null, bool clearBackStack = false, NavigationTransitionInfo infoOverride = null)
        {
            throw new NotImplementedException();
        }

        public bool Navigate<T>(T key, object parameter = null, bool clearBackStack = false, NavigationTransitionInfo infoOverride = null) where T : struct, IConvertible
        {
            throw new NotImplementedException();
        }

        public Task OpenAsync(
            Type page,
            object parameter = null,
            string title = null,
            ViewSizePreference size = ViewSizePreference.UseHalf)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public bool RestoreSavedNavigation()
        {
            throw new NotImplementedException();
        }

        public void Resuming()
        {
            throw new NotImplementedException();
        }

        public void SaveNavigation()
        {
            throw new NotImplementedException();
        }

        public void Show(SettingsFlyout flyout, string parameter = null)
        {
            throw new NotImplementedException();
        }

        public Task SuspendingAsync()
        {
            throw new NotImplementedException();
        }
    }
}