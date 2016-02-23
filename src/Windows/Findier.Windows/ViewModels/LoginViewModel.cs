using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private bool _isLoading;
        private string _password;
        private bool _requireAuthentication;
        private string _username;

        public LoginViewModel(IFindierService findierService)
        {
            _findierService = findierService;
            LoginCommand = new DelegateCommand(LoginExecute);
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                Set(ref _isLoading, value);
            }
        }

        public DelegateCommand LoginCommand { get; }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                Set(ref _password, value);
            }
        }

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                Set(ref _username, value);
            }
        }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            _requireAuthentication = parameter as bool? ?? false;
        }

        private async void LoginExecute()
        {
            IsLoading = true;
            var restResponse = await _findierService.LoginAsync(new OAuthRequest(Username, Password));
            IsLoading = false;

            if (restResponse.IsSuccessStatusCode)
            {
                CurtainPrompt.Show("Welcome!");

                if (_requireAuthentication)
                {
                    // return to original page that requested authentication
                    NavigationService.Frame.BackStack.RemoveAt(NavigationService.Frame.BackStack.Count - 1);
                    NavigationService.GoBack();
                }
                else
                {
                    NavigationService.Navigate(typeof (MainPage), clearBackStack: true);
                }
            }
            else
            {
                CurtainPrompt.ShowError(restResponse.DeserializedResponse?.Error
                    ?? "Problem with login. Try again later.");
            }
        }
    }
}