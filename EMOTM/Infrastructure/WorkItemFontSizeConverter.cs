using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    internal class WorkItemFontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState state = (WindowState) value;
            if (state == WindowState.Maximized)
            {
                return 32;
            }
            return 14;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}