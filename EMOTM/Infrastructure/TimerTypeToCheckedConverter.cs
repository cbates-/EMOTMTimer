using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    internal class TimerTypeToCheckedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = false;
            ret = ((TimerType) value == TimerType.AMRAP) ? true : false;
            if (parameter != null)
            {
                string p = parameter as string;
                System.Diagnostics.Debug.WriteLine("p: {0}", p.GetType());
                if (String.Equals(p, "false"))
                {
                    ret = !ret;
                }
            }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}