using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Findier.Windows.Controls
{
    public class MaterialCard : ContentControl
    {
        public static readonly DependencyProperty ActionButtonCommandProperty =
            DependencyProperty.RegisterAttached(nameof(ActionButtonCommand),
                typeof (ICommand),
                typeof (MaterialCard),
                null);

        public static readonly DependencyProperty ActionButtonFlyoutProperty =
            DependencyProperty.RegisterAttached(nameof(ActionButtonFlyout),
                typeof (FlyoutBase),
                typeof (MaterialCard),
                null);

        public static readonly DependencyProperty ActionButtonTextProperty =
            DependencyProperty.RegisterAttached(nameof(ActionButtonText),
                typeof (string),
                typeof (MaterialCard),
                new PropertyMetadata("See More"));

        public static readonly DependencyProperty ActionButtonVisibilityProperty =
            DependencyProperty.RegisterAttached(nameof(ActionButtonVisibility),
                typeof (Visibility),
                typeof (MaterialCard),
                new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.RegisterAttached(nameof(Header),
                typeof (string),
                typeof (MaterialCard),
                null);

        public static readonly DependencyProperty IsActionButtonEnabledProperty =
            DependencyProperty.RegisterAttached(nameof(IsActionButtonEnabled),
                typeof (bool),
                typeof (MaterialCard),
                new PropertyMetadata(true));

        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.RegisterAttached(nameof(IsLoading),
                typeof (bool),
                typeof (MaterialCard),
                new PropertyMetadata(false));

        public MaterialCard()
        {
            DefaultStyleKey = typeof (MaterialCard);
        }

        public event RoutedEventHandler ActionButtonClick;

        public ICommand ActionButtonCommand
        {
            get
            {
                return (ICommand)GetValue(ActionButtonCommandProperty);
            }
            set
            {
                SetValue(ActionButtonCommandProperty, value);
            }
        }

        public FlyoutBase ActionButtonFlyout
        {
            get
            {
                return (FlyoutBase)GetValue(ActionButtonFlyoutProperty);
            }
            set
            {
                SetValue(ActionButtonFlyoutProperty, value);
            }
        }

        public string ActionButtonText
        {
            get
            {
                return (string)GetValue(ActionButtonTextProperty);
            }
            set
            {
                SetValue(ActionButtonTextProperty, value);
            }
        }

        public Visibility ActionButtonVisibility
        {
            get
            {
                return (Visibility)GetValue(ActionButtonVisibilityProperty);
            }
            set
            {
                SetValue(ActionButtonVisibilityProperty, value);
            }
        }

        public string Header
        {
            get
            {
                return (string)GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public bool IsActionButtonEnabled
        {
            get
            {
                return (bool)GetValue(IsActionButtonEnabledProperty);
            }
            set
            {
                SetValue(IsActionButtonEnabledProperty, value);
            }
        }

        public bool IsLoading
        {
            get
            {
                return (bool)GetValue(IsLoadingProperty);
            }
            set
            {
                SetValue(IsLoadingProperty, value);
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var button = GetTemplateChild("PART_ACTION_BUTTON") as Button;
            if (button != null)
            {
                button.Click += (sender, args) =>
                    {
                        ActionButtonClick?.Invoke(this, args);
                        ActionButtonCommand?.Execute(this);
                    };
            }
        }
    }
}