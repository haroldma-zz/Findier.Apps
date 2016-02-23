using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class AuthenticationViewModel : ViewModelBase
    {
        private bool _requireAuthentication;

        public AuthenticationViewModel()
        {
            BrowseAnonymouslyCommand = new DelegateCommand(BrowseAnonymouslyExecute);
            LoginCommand = new DelegateCommand(LoginExecute);
            RegisterCommand = new DelegateCommand(RegisterExecute);
        }

        public DelegateCommand BrowseAnonymouslyCommand { get; }

        public DelegateCommand LoginCommand { get; }

        public DelegateCommand RegisterCommand { get; set; }

        public bool RequireAuthentication
        {
            get
            {
                return _requireAuthentication;
            }
            set
            {
                Set(ref _requireAuthentication, value);
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            RequireAuthentication = parameter as bool? ?? false;
        }

        private void BrowseAnonymouslyExecute()
        {
            NavigationService.Navigate(typeof (MainPage));
        }

        private void LoginExecute()
        {
            NavigationService.Navigate(typeof (LoginPage), RequireAuthentication);
        }

        private void RegisterExecute()
        {
            NavigationService.Navigate(typeof (RegisterPage), RequireAuthentication);
        }
    }
}