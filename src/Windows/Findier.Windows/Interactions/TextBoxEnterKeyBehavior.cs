﻿using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Microsoft.Xaml.Interactivity;

namespace Findier.Windows.Interactions
{
    [ContentProperty(Name = "Actions")]
    [TypeConstraint(typeof (TextBox))]
    public class TextBoxEnterKeyBehavior : DependencyObject, IBehavior
    {
        public static readonly DependencyProperty ActionsProperty =
            DependencyProperty.Register("Actions", typeof (ActionCollection),
                typeof (TextBoxEnterKeyBehavior), new PropertyMetadata(null));

        private TextBox AssociatedTextBox => AssociatedObject as TextBox;

        public ActionCollection Actions
        {
            get
            {
                var actions = (ActionCollection) GetValue(ActionsProperty);
                if (actions == null)
                {
                    SetValue(ActionsProperty, actions = new ActionCollection());
                }
                return actions;
            }
        }

        public DependencyObject AssociatedObject { get; private set; }

        public void Attach(DependencyObject associatedObject)
        {
            AssociatedObject = associatedObject;
            AssociatedTextBox.KeyDown -= AssociatedTextBox_KeyDown;
            AssociatedTextBox.KeyDown += AssociatedTextBox_KeyDown;
        }

        public void Detach()
        {
            AssociatedTextBox.KeyDown -= AssociatedTextBox_KeyDown;
        }

        private void AssociatedTextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            // winrt has a weird issue with the enter key, it is called twice in debug, hence the repeat count check
            if (e.Key == VirtualKey.Enter && e.KeyStatus.RepeatCount == 0)
            {
                Interaction.ExecuteActions(AssociatedObject, Actions, AssociatedTextBox.Text);
            }
        }
    }
}