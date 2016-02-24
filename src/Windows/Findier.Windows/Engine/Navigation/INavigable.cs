using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Findier.Core.Utilities.Interfaces;

namespace Findier.Windows.Engine.Navigation
{
    // DOCS: https://github.com/Windows-XAML/Template10/wiki/Docs-%7C-NavigationService
    public interface INavigable
    {
        IDispatcherUtility Dispatcher { get; set; }

        INavigationService NavigationService { get; set; }

        IStateItems SessionState { get; set; }

        void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state);

        void OnNavigatingFrom(NavigatingEventArgs args);

        void OnSaveState(IDictionary<string, object> state, bool suspending);

        Task OnSaveStateAsync(IDictionary<string, object> state, bool suspending);
    }
}