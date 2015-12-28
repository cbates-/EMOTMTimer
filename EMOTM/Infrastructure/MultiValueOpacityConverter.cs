using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using EMOTM.ViewModel;

namespace EMOTM.Infrastructure
{
	public class MultiValueOpacityConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var name = (string)values[0];
			var state = (TimerState)values[1];
			var whichMin = (ThisThatMin)values[2];

			switch (state)
			{
				case TimerState.Started:
					{

						return name == whichMin.ToString() ? 1.0 : 0.5;
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
