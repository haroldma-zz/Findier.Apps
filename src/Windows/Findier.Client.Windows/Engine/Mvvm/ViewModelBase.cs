﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Navigation;
using Findier.Client.Windows.Engine.Navigation;
using Findier.Core.Utilities.Interfaces;
using Newtonsoft.Json;

namespace Findier.Client.Windows.Engine.Mvvm
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-MVVM
    public abstract class ViewModelBase : BindableBase, INavigable
    {
        [JsonIgnore]
        public IDispatcherUtility Dispatcher { get; set; }

        [JsonIgnore]
        public INavigationService NavigationService { get; set; }

        [JsonIgnore]
        public IStateItems SessionState { get; set; }

        public bool IsInDesignMode => DesignMode.DesignModeEnabled;

        public virtual async Task OnSaveStateAsync(IDictionary<string, object> state, bool suspending)
        {
            await Task.CompletedTask;
        }

        public virtual void OnSaveState(IDictionary<string, object> state, bool suspending)
        {
        }

        public virtual void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            /* nothing by default */
        }

        public virtual void OnNavigatingFrom(NavigatingEventArgs args)
        {
            /* nothing by default */
        }
    }
}