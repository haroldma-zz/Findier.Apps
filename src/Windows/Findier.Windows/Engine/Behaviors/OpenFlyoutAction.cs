using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Microsoft.Xaml.Interactivity;

namespace Findier.Windows.Engine.Behaviors
{
    public class OpenFlyoutAction : DependencyObject, IAction
    {
        public object Execute(object sender, object parameter)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            return null;
        }
    }

}
