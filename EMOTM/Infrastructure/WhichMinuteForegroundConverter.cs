using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using EMOTM.ViewModel;

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

		#region IMultiValueConverter Members

		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush ret = grayBrush;


			var name = (string) values[0];
			var dc = (MainViewModel) values[1];

			switch (dc.TimerState)
			{
				case TimerState.Stopped:
					ret = grayBrush;
					break;

				default:
				{
					switch (dc.WhichMinute)
					{
						case ThisThatMin.ThisMinute:
							ret = (String.Equals(name, ThisThatMin.ThisMinute.ToString())) ? blueBrush : grayBrush;
							break;
						case ThisThatMin.ThatMinute:
							ret = (String.Equals(name, ThisThatMin.ThatMinute.ToString())) ? blueBrush : grayBrush;
							break;
						case ThisThatMin.TheOtherMinute:
							ret = (String.Equals(name, ThisThatMin.TheOtherMinute.ToString())) ? blueBrush : grayBrush;
							break;
					}
				}
					break;
			}

			return ret;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}