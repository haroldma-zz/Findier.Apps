using Findier.Windows.Engine;

namespace Findier.Windows.ViewModels
{
    internal class ViewModelLocator
    {
        public static AppKernel Kernel => App.Current?.Kernel ?? AppKernelFactory.Create();

        public AuthenticationViewModel Authentication => Kernel.Resolve<AuthenticationViewModel>();

        public EditPostViewModel EditPost => Kernel.Resolve<EditPostViewModel>();

        public FinboardViewModel Finboard => Kernel.Resolve<FinboardViewModel>();

        public LoginViewModel Login => Kernel.Resolve<LoginViewModel>();

        public MainViewModel Main => Kernel.Resolve<MainViewModel>();

        public NewCommentViewModel NewComment => Kernel.Resolve<NewCommentViewModel>();

        public NewPostViewModel NewPost => Kernel.Resolve<NewPostViewModel>();

        public PostViewModel Post => Kernel.Resolve<PostViewModel>();

        public RegisterViewModel Register => Kernel.Resolve<RegisterViewModel>();
    }
}