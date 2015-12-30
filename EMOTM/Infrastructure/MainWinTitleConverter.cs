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
	class MainWinTitleConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string s = "EMOTM";
			MainViewModel mvm = parameter as MainViewModel;
			if (mvm != null)
			{
				s = string.Format("E{0}MOT{0}M", (mvm.LengthOfMinute > 1) ? mvm.LengthOfMinute.ToString() : string.Empty);
			}
			return s;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}