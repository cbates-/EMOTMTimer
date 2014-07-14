using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    internal class WhichMinOpacityConverter : IValueConverter
    {
        private readonly double activeOpacity = 1.0;
        private readonly double inActiveOpacity = 0.30;

        // Parameter is the name of the control
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double ret = 1.0;
            ThisThatMin whichMin = (ThisThatMin)value;
            string s = parameter as string;

            switch (whichMin)
            {
                case ThisThatMin.ThisMinute:
                    ret = (String.Equals(s, ThisThatMin.ThisMinute.ToString())) ? activeOpacity : inActiveOpacity;
                    break;

                case ThisThatMin.ThatMinute:
                    ret = (String.Equals(s, ThisThatMin.ThatMinute.ToString())) ? activeOpacity : inActiveOpacity;
                    break;
                case ThisThatMin.TheOtherMinute:
                    ret = (String.Equals(s, ThisThatMin.TheOtherMinute.ToString())) ? activeOpacity : inActiveOpacity;
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