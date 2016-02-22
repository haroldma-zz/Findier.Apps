using System.Collections.Generic;
using Windows.UI.Xaml.Navigation;
using Findier.Client.Windows.Common;
using Findier.Client.Windows.Engine.Mvvm;
using Findier.Client.Windows.Views;
using Findier.Web.Requests;
using Findier.Web.Services;

namespace Findier.Client.Windows.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private readonly IFindierService _findierService;
        private string _displayName;
        private string _email;
        private bool _isLoading;
        private string _password;
        private bool _requireAuthentication;
        private string _username;

        public RegisterViewModel(IFindierService findierService)
        {
            _findierService = findierService;
            RegisterCommand = new DelegateCommand(RegisterExecute);
        }

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                Set(ref _displayName, value);
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Set(ref _email, value);
            }
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

        public DelegateCommand RegisterCommand { get; }

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

        private async void RegisterExecute()
        {
            IsLoading = true;
            var restResponse = await _findierService.RegisterAsync(
                new CreateUserRequest(Username, Email, DisplayName, Password));
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
                    ?? "Problem creating account. Try again later.");
            }
        }
    }
}