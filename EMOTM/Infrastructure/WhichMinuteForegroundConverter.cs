using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace EMOTM.Infrastructure
{
    class WhichMinuteForegroundConverter : IValueConverter
    {

        private readonly SolidColorBrush blueBrush;
        private readonly SolidColorBrush grayBrush;
        public WhichMinuteForegroundConverter()
        {
            blueBrush = new SolidColorBrush(Colors.Blue);
            grayBrush = new SolidColorBrush(Colors.DarkGray);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush ret = grayBrush;
            ThisThatMin whichMin = (ThisThatMin) value;
            string s = parameter as string;

            switch (whichMin)
            {
                case ThisThatMin.ThisMinute:
                    ret = (String.Equals(s, ThisThatMin.ThisMinute.ToString())) ? blueBrush : grayBrush;
                    break;
                case ThisThatMin.ThatMinute:
                    ret = (String.Equals(s, ThisThatMin.ThatMinute.ToString())) ? blueBrush : grayBrush;
                    break;
                case ThisThatMin.TheOtherMinute:
                    ret = (String.Equals(s, ThisThatMin.TheOtherMinute.ToString())) ? blueBrush : grayBrush;
                    break;
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return grayBrush;
        }
    }
}
