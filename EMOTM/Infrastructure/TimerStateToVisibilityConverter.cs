using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
    internal class TimerStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility ret = Visibility.Collapsed;
	        switch ((TimerState) value)
	        {
			        case TimerState.Stopped:
					ret = Visibility.Visible;
			        break;
					
					case TimerState.Started:
					case TimerState.Paused:
					ret = Visibility.Collapsed;
			        break;

				default:
					ret = Visibility.Collapsed;
			        break;
	        }
            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}