using System;
using System.Globalization;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
	class StopBtnCaptionConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string caption = "Stop";
			// value is TimerState
			// targetType is string
			TimerState ts = (TimerState) value;
			switch (ts)
			{
				case TimerState.Started:
					break;
				case TimerState.Stopped:
					caption = "Stop";
					break;
				case TimerState.Paused:
					// caption = "Reset";
					caption = "Stop";
					break;
				default:
					break;
			}
			return caption;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}