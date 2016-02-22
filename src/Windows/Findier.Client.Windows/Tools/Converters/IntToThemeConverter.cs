﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Findier.Client.Windows.Tools.Converters
{
    public class IntToThemeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var i = (int) value;
            return (ElementTheme) i;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var i = (ElementTheme)value;
            return (int)i;
        }
    }
}