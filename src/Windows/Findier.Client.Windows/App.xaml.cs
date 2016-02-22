using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Findier.Client.Windows.Common;
using Findier.Client.Windows.Views;
using Findier.Core.Utilities.Interfaces;
using Findier.Web.Services;
using Microsoft.ApplicationInsights;

namespace Findier.Client.Windows
{
    sealed partial class App
    {
        /// <summary>
        ///     Initializes the singleton application object.  This is the first line of authored code
        ///     executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            WindowsAppInitializer.InitializeAsync(
                WindowsCollectors.Metadata |
                    WindowsCollectors.Session);
            InitializeComponent();
        }

        public static new App Current => Application.Current as App;

        // runs even if restored from state
        public override async Task OnInitializeAsync(IActivatedEventArgs args)
        {
            // Wrap the frame in the shell (hamburger menu)
            /* Shell = new Shell();
            Window.Current.Content = Shell;*/

            await Task.CompletedTask;
        }

        // runs only when not restored from state
        public override Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            var findierService = Kernel.Resolve<IFindierService>();
            var applicationUtility = Kernel.Resolve<IApplicationUtility>();

            NavigationService.Navigate(findierService.IsAuthenticated || !applicationUtility.IsFirstLaunch
                ? typeof (MainPage)
                : typeof (AuthenticationPage));

            return Task.FromResult(0);
        }

        protected override bool OnUnhandledException(Exception ex)
        {
            CurtainPrompt.ShowError("Crash prevented",
                () =>
                    {
                        MessageBox.Show(
                            "The problem has been reported.  If you continue to experience this bug, email support.  Details: "
                                +
                                ex.Message,
                            "Crash prevented");
                    });
            return true;
        }
    }
}