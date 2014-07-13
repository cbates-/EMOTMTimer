using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    class ColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int cnt = (int)value;
            return cnt == 1 ? 2 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}