using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    class ColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colSpan = 1;
            ListCntEnums cnt = (ListCntEnums)value;
            return cnt == ListCntEnums.One ? 2 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}