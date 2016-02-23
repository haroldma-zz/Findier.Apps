﻿#region

using System;
using Windows.UI.Xaml.Data;

#endregion

namespace Findier.Windows.Tools.Converters
{
    public class TimespanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            return ((TimeSpan) value).ToString("mm\\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotImplementedException();
        }
    }
}