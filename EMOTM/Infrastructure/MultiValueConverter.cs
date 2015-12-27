using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
	public class MultiValueConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var one = (ThisThatMin)values[0];
			var two = (TimerState)values[1];
			switch (two)
			{
				case TimerState.Started:
					{
						switch (one)
						{
							case ThisThatMin.ThisMinute:
								break;
							case ThisThatMin.ThatMinute:
								break;
							case ThisThatMin.TheOtherMinute:
								break;
						}
					}
					break;
				default:
					return 0.90;
					break;
			}
			return null;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
