﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    class ThatMinVisConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value == 1) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Visibility.Visible;
        }
    }
}