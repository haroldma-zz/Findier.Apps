using Findier.Client.Windows.Engine;

namespace Findier.Client.Windows.ViewModels
{
    internal class ViewModelLocator
    {
        public static AppKernel Kernel => App.Current?.Kernel ?? AppKernelFactory.Create();

        public AuthenticationViewModel Authentication => Kernel.Resolve<AuthenticationViewModel>();

        public FinboardViewModel Finboard => Kernel.Resolve<FinboardViewModel>();

        public LoginViewModel Login => Kernel.Resolve<LoginViewModel>();

        public MainViewModel Main => Kernel.Resolve<MainViewModel>();

        public PostViewModel Post => Kernel.Resolve<PostViewModel>();

        public RegisterViewModel Register => Kernel.Resolve<RegisterViewModel>();
    }
}