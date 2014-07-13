using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    class WhichMinOpacityConverter : IValueConverter
    {
        // Parameter is the name of the control
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 1.0;
            ThisThatMin whichMin = (ThisThatMin)value;
            string s = parameter as string;

            switch (whichMin)
            {
                case ThisThatMin.ThisMinute:
                    ret = (String.Equals(s, ThisThatMin.ThisMinute.ToString())) ? 1.0 : 0.2;
                    break;
                case ThisThatMin.ThatMinute:
                    ret = (String.Equals(s, ThisThatMin.ThatMinute.ToString())) ? 1.0 : 0.2;
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 1.0;
        }
    }
}
