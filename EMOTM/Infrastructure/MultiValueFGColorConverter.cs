using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using EMOTM.ViewModel;

namespace EMOTM.Infrastructure
{
	public class MultiValueFgColorConverter : IMultiValueConverter
	{
		private readonly SolidColorBrush blueBrush;
		private readonly SolidColorBrush grayBrush;
		private readonly SolidColorBrush blackBrush;

		public MultiValueFgColorConverter()
		{
			blueBrush = new SolidColorBrush(Colors.Blue);
			grayBrush = new SolidColorBrush(Colors.DarkGray);
			blackBrush = new SolidColorBrush(Colors.Black);
		}
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			SolidColorBrush ret = grayBrush;

			var name = (string)values[0];
			var state = (TimerState)values[1];
			var whichMin = (ThisThatMin)values[2];

			switch (state)
			{
				//case TimerState.Paused:
				case TimerState.Stopped:
					ret = blackBrush;
					break;

				default:
					{
						switch (whichMin)
						{
							case ThisThatMin.ThisMinute:
								ret = (name.Contains(ThisThatMin.ThisMinute.ToString()) ) ? blueBrush : grayBrush;
								break;
							case ThisThatMin.ThatMinute:
								ret = (name.Contains(ThisThatMin.ThatMinute.ToString()) ) ? blueBrush : grayBrush;
								break;
							case ThisThatMin.TheOtherMinute:
								ret = (name.Contains(ThisThatMin.TheOtherMinute.ToString()) ) ? blueBrush : grayBrush;
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
	}
}
