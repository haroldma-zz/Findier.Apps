using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Email;
using Windows.ApplicationModel.Store;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Findier.Web.Enums;
using Findier.Web.Models;
using Findier.Web.Requests;
using Findier.Web.Services;
using Findier.Windows.Common;
using Findier.Windows.Engine.Mvvm;
using Findier.Windows.IncrementalLoading;
using Findier.Windows.Views;

namespace Findier.Windows.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private FinboardCollection _finboardCollection;

        public MainViewModel(IFindierService findierService)
        {
            FindierService = findierService;
            FinboardClickCommand = new DelegateCommand<ItemClickEventArgs>(FinboardClickExecute);
            LoginCommand = new DelegateCommand(LoginExecute);
            LogoutCommand = new DelegateCommand(LogoutExecute);
            ContactCommand = new DelegateCommand(ContactExecute);
            ReviewCommand = new DelegateCommand(ReviewExecute);

            if (IsInDesignMode)
            {
                OnNavigatedTo(null, NavigationMode.New, new Dictionary<string, object>());
            }
        }

        public DelegateCommand ContactCommand { get; }

        public DelegateCommand<ItemClickEventArgs> FinboardClickCommand { get; }

        public FinboardCollection FinboardCollection
        {
            get
            {
                return _finboardCollection;
            }
            set
            {
                Set(ref _finboardCollection, value);
            }
        }

        public IFindierService FindierService { get; }

        public DelegateCommand LoginCommand { get; }

        public DelegateCommand LogoutCommand { get; }

        public DelegateCommand ReviewCommand { get; }

        public override sealed void OnNavigatedTo(
            object parameter,
            NavigationMode mode,
            IDictionary<string, object> state)
        {
            FinboardCollection = new FinboardCollection(new GetFinboardsRequest(Country.PR).Limit(20), FindierService);
        }

        private async void ContactExecute()
        {
            var mail = new EmailMessage
            {
                Subject = "Findier for Windows"
            };
            mail.To.Add(new EmailRecipient("help@zumicts.com", "Zumicts Support"));
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }

        private void FinboardClickExecute(ItemClickEventArgs args)
        {
            var finboard = (Finboard)args.ClickedItem;
            NavigationService.Navigate(typeof (FinboardPage), finboard);
        }

        private void LoginExecute()
        {
            NavigationService.Navigate(typeof (AuthenticationPage), clearBackStack: true);
        }

        private void LogoutExecute()
        {
            FindierService.Logout();
            NavigationService.Navigate(typeof (AuthenticationPage), clearBackStack: true);
            CurtainPrompt.Show("Goodbye!");
        }

        private async void ReviewExecute()
        {
            await Launcher.LaunchUriAsync(new Uri("ms-windows-store:reviewapp?appid=" + CurrentApp.AppId));
        }
    }
}