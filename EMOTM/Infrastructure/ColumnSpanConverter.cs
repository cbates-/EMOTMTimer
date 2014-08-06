using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    internal class ColumnSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int colWidth = 1;
            ListCntEnums cnt = (ListCntEnums) value;
            string whichCol = (string) parameter;
            switch (whichCol)
            {
                case "One":
                    colWidth = 1;
                    break;
                case "Two":
                    colWidth = (cnt == ListCntEnums.Two) || (cnt == ListCntEnums.Three) ? 1 : 0;
                    break;
                case "Three":
                    colWidth = (cnt == ListCntEnums.Three) ? 1 : 0;
                    break;
            }
            return colWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1;
        }
    }
}