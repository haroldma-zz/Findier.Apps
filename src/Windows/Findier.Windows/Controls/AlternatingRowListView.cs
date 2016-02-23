﻿using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Findier.Windows.Common;
using Findier.Windows.Tools;

namespace Findier.Windows.Controls
{
    public class AlternatingRowListView : ScrollListView
    {
        public static readonly DependencyProperty OddRowBackgroundProperty =
            DependencyProperty.Register("OddRowBackground", typeof (Brush), typeof (AlternatingRowListView), null);

        public static readonly DependencyProperty EvenRowBackgroundProperty =
            DependencyProperty.Register("EvenRowBackground", typeof (Brush), typeof (AlternatingRowListView), null);

        public Brush OddRowBackground

        {
            get { return (Brush) GetValue(OddRowBackgroundProperty); }

            set { SetValue(OddRowBackgroundProperty, value); }
        }

        public Brush EvenRowBackground

        {
            get { return (Brush) GetValue(EvenRowBackgroundProperty); }

            set { SetValue(EvenRowBackgroundProperty, value); }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var listViewItem = element as ListViewItem;

            if (listViewItem != null)

            {
                var index = IndexFromContainer(element);

                var isOdd = (index + 1)%2 == 1;

                // support for adjusting to groups (each group should be threated individually)
                var collectionViewSource = Tag as CollectionViewSource;
                var groups = collectionViewSource?.Source as IEnumerable<AlphaKeyGroup>;
                if (groups != null)
                {
                    var o = Items?[index];
                    if (o != null)
                    {
                        var currentGroup = groups.FirstOrDefault(p => p.Contains(o));
                        index = currentGroup.IndexOf(o);
                        isOdd = (index + 1)%2 == 1;
                    }
                }

                listViewItem.Background = isOdd
                    ? OddRowBackground ?? new SolidColorBrush((Color) App.Current.Resources["SystemChromeLowColor"])
                    : EvenRowBackground;
            }
        }
    }
}