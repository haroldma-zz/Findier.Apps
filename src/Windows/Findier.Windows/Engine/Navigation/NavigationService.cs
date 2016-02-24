using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Findier.Core.Extensions;
using Findier.Core.Utilities.Interfaces;
using Findier.Core.Windows.Utilities;
using Findier.Windows.Services;

namespace Findier.Windows.Engine.Navigation
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public class NavigationService : INavigationService
    {
        private const string EmptyNavigation = "1,0";

        public NavigationService(Frame frame)
        {
            FrameFacade = new FrameFacade(frame);
            FrameFacade.Navigating += async (s, e) =>
                {
                    if (e.Suspending)
                    {
                        return;
                    }

                    // allow the viewmodel to cancel navigation
                    e.Cancel = !NavigatingFrom(false);
                    if (!e.Cancel)
                    {
                        await NavigateFromAsync(false);
                    }
                };
            FrameFacade.Navigated += (s, e) => { NavigateTo(e.NavigationMode, e.Parameter); };
        }

        public event TypedEventHandler<Type> AfterRestoreSavedNavigation;

        public bool CanGoBack => FrameFacade.CanGoBack;

        public bool CanGoForward => FrameFacade.CanGoForward;

        public object CurrentPageParam => FrameFacade.CurrentPageParam;

        public Type CurrentPageType => FrameFacade.CurrentPageType;

        public IDispatcherUtility Dispatcher => WindowWrapper.Current(this).Dispatcher;

        public Frame Frame => FrameFacade.Frame;

        public FrameFacade FrameFacade { get; }

        private object LastNavigationParameter { get; set; }

        private string LastNavigationType { get; set; }

        public void ClearCache(bool removeCachedPagesInBackStack = false)
        {
            var currentSize = FrameFacade.Frame.CacheSize;

            if (removeCachedPagesInBackStack)
            {
                FrameFacade.Frame.CacheSize = 0;
            }
            else
            {
                if (Frame.BackStackDepth == 0)
                {
                    Frame.CacheSize = 1;
                }
                else
                {
                    Frame.CacheSize = Frame.BackStackDepth;
                }
            }

            FrameFacade.Frame.CacheSize = currentSize;
        }

        public void ClearHistory()
        {
            FrameFacade.Frame.BackStack.Clear();
        }

        public void GoBack()
        {
            if (FrameFacade.CanGoBack)
            {
                FrameFacade.GoBack();
            }
        }

        public void GoForward()
        {
            FrameFacade.GoForward();
        }

        public bool Navigate(
            Type page,
            object parameter = null,
            bool clearBackStack = false,
            NavigationTransitionInfo infoOverride = null)
        {
            if (page == null)
            {
                throw new ArgumentNullException(nameof(page));
            }
            if (page.FullName.Equals(LastNavigationType))
            {
                if (parameter == LastNavigationParameter)
                {
                    return false;
                }

                if (parameter != null && parameter.Equals(LastNavigationParameter))
                {
                    return false;
                }
            }

            parameter = SerializePageParam(parameter);
            var result = FrameFacade.Navigate(page, parameter, infoOverride);
            if (result && clearBackStack)
            {
                Frame.BackStack.Clear();
            }
            return result;
        }

        /*
            Navigate<T> allows developers to navigate using a
            page key instead of the view type. This is accomplished by
            creating a custom Enum and setting up the PageKeys dict
            with the Key/Type pairs for your views. The dict is
            shared by all NavigationServices and is stored in
            the BootStrapper (or Application) of the app.

            Implementation example:

            // define your Enum
            public Enum Pages { MainPage, DetailPage }

            // setup the keys dict
            var keys = BootStrapper.PageKeys<Views>();
            keys.Add(Pages.MainPage, typeof(Views.MainPage));
            keys.Add(Pages.DetailPage, typeof(Views.DetailPage));

            // use Navigate<T>()
            NavigationService.Navigate(Pages.MainPage);
        */

        // T must be the same custom Enum used with BootStrapper.PageKeys()
        public bool Navigate<T>(
            T key,
            object parameter = null,
            bool clearBackStack = false,
            NavigationTransitionInfo infoOverride = null)
            where T : struct, IConvertible
        {
            var keys = BootStrapper.Current.PageKeys<T>();
            if (!keys.ContainsKey(key))
            {
                throw new KeyNotFoundException(key.ToString(CultureInfo.InvariantCulture));
            }
            var page = keys[key];
            if (page.FullName.Equals(LastNavigationType)
                && parameter == LastNavigationParameter)
            {
                return false;
            }
            var result = FrameFacade.Navigate(page, parameter, infoOverride);
            if (result && clearBackStack)
            {
                Frame.BackStack.Clear();
            }
            return result;
        }

        public async Task OpenAsync(
            Type page,
            object parameter = null,
            string title = null,
            ViewSizePreference size = ViewSizePreference.UseHalf)
        {
            var currentView = ApplicationView.GetForCurrentView();
            title = title ?? currentView.Title;

            var newView = CoreApplication.CreateNewView();
            var dispatcher = new DispatcherUtility(newView.Dispatcher);
            await dispatcher.RunAsync(async () =>
                {
                    var newWindow = Window.Current;
                    var newAppView = ApplicationView.GetForCurrentView();
                    newAppView.Title = title;

                    var frame = BootStrapper.Current.NavigationServiceSetup(BootStrapper.BackButton.Ignore,
                        BootStrapper.ExistingContent.Exclude,
                        new Frame());
                    frame.Navigate(page, parameter);
                    newWindow.Content = frame.Frame;
                    newWindow.Activate();

                    await ApplicationViewSwitcher
                        .TryShowAsStandaloneAsync(newAppView.Id, ViewSizePreference.Default, currentView.Id, size);
                });
        }

        public void Refresh()
        {
            FrameFacade.Refresh();
        }

        public bool RestoreSavedNavigation()
        {
            try
            {
                var state = FrameFacade.PageStateContainer(GetType());
                if (state == null || !state.Any() || !state.ContainsKey("CurrentPageType"))
                {
                    return false;
                }

                FrameFacade.CurrentPageType = Type.GetType(state["CurrentPageType"].ToString());
                FrameFacade.CurrentPageParam = DeserializePageParam(state["CurrentPageParam"]?.ToString());
                FrameFacade.SetNavigationState(state["NavigateState"]?.ToString());
                NavigateTo(NavigationMode.Refresh, FrameFacade.CurrentPageParam);
                while (Frame.Content == null)
                {
                    Task.Yield().GetAwaiter().GetResult();
                }
                AfterRestoreSavedNavigation?.Invoke(this, FrameFacade.CurrentPageType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Resuming()
        {
            /* nothing */
        }

        public void SaveNavigation()
        {
            if (CurrentPageType == null)
            {
                return;
            }

            var state = FrameFacade.PageStateContainer(GetType());
            if (state == null)
            {
                throw new InvalidOperationException("State container is unexpectedly null");
            }

            state["CurrentPageType"] = CurrentPageType.AssemblyQualifiedName;
            state["CurrentPageParam"] = SerializePageParam(CurrentPageParam);
            state["NavigateState"] = FrameFacade?.GetNavigationState();
        }

        public void Show(SettingsFlyout flyout, string parameter = null)
        {
            if (flyout == null)
            {
                throw new ArgumentNullException(nameof(flyout));
            }
            var dataContext = flyout.DataContext as INavigable;
            if (dataContext != null)
            {
                dataContext.OnNavigatedTo(parameter, NavigationMode.New, null);
            }
            flyout.Show();
        }

        public async Task SuspendingAsync()
        {
            SaveNavigation();
            await NavigateFromAsync(true);
        }

        protected virtual object DeserializePageParam(string pageParam)
        {
            return pageParam.TryDeserializeJsonWithTypeInfo();
        }

        protected virtual string SerializePageParam(object pageParam)
        {
            return pageParam.SerializeToJsonWithTypeInfo();
        }

        // after navigate
        private async Task NavigateFromAsync(bool suspending)
        {
            var page = FrameFacade.Content as Page;
            // call viewmodel
            var dataContext = page?.DataContext as INavigable;
            if (dataContext != null)
            {
                var pageState = FrameFacade.PageStateContainer(page.GetType());
                await dataContext.OnSaveStateAsync(pageState, suspending);
                dataContext.OnSaveState(pageState, suspending);
            }
        }

        private void NavigateTo(NavigationMode mode, object parameter)
        {
            parameter = DeserializePageParam(parameter as string);

            LastNavigationParameter = parameter;
            LastNavigationType = FrameFacade.Content.GetType().FullName;

            if (mode == NavigationMode.New)
            {
                FrameFacade.ClearFrameState();
            }

            var page = FrameFacade.Content as Page;
            if (page == null)
            {
                return;
            }

            var insightsService = App.Current.Kernel.Resolve<IInsightsService>();
            insightsService.TrackPageView(page.GetType().Name);

            if (page.DataContext == null)
            {
                // to support dependency injection, but keeping it optional.
                var viewmodel = BootStrapper.Current.ResolveForPage(page.GetType(), this);
                if (viewmodel != null)
                {
                    page.DataContext = viewmodel;
                }
            }

            // call viewmodel
            var dataContext = page.DataContext as INavigable;
            if (dataContext != null)
            {
                // prepare for state load
                dataContext.NavigationService = this;
                dataContext.Dispatcher = WindowWrapper.Current(this)?.Dispatcher;
                dataContext.SessionState = BootStrapper.Current.SessionState;
                var pageState = FrameFacade.PageStateContainer(page.GetType());
                dataContext.OnNavigatedTo(parameter, mode, pageState);
            }
        }

        // before navigate (cancellable) 
        private bool NavigatingFrom(bool suspending)
        {
            var page = FrameFacade.Content as Page;
            if (page != null)
            {
                // force (x:bind) page bindings to update
                var fields = page.GetType().GetRuntimeFields();
                var bindings = fields.FirstOrDefault(x => x.Name.Equals("Bindings"));
                if (bindings != null)
                {
                    var update = bindings.GetType().GetTypeInfo().GetDeclaredMethod("Update");
                    update?.Invoke(bindings, null);
                }

                // call navagable override (navigating)
                var dataContext = page.DataContext as INavigable;
                if (dataContext != null)
                {
                    var args = new NavigatingEventArgs
                    {
                        NavigationMode = FrameFacade.NavigationModeHint,
                        PageType = FrameFacade.CurrentPageType,
                        Parameter = FrameFacade.CurrentPageParam,
                        Suspending = suspending
                    };
                    dataContext.OnNavigatingFrom(args);
                    return !args.Cancel;
                }
            }
            return true;
        }
    }
}