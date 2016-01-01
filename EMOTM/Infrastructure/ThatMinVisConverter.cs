using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
	internal class ThatMinVisConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility vis = Visibility.Collapsed;
			ListCntEnums cnt = (ListCntEnums) value;
			string whichCol = (string) parameter;
#if ZIGGY
            switch (cnt)
            {
                case ListCntEnums.One:
                    vis = Visibility.Visible;
                    break;
                case ListCntEnums.Two:
                    vis = (string.Equals(whichCol, "Two") || string.Equals(whichCol, "Three")) ? Visibility.Visible : Visibility.Collapsed;
                    break;
                case ListCntEnums.Three:
                    vis = (string.Equals(whichCol, "Three")) ? Visibility.Visible : Visibility.Collapsed;
                    break;

            }
#else
			switch (whichCol)
			{
				case "One":
					vis = Visibility.Visible;
					break;
				case "Two":
					vis = (cnt == ListCntEnums.Two) || (cnt == ListCntEnums.Three)
						? Visibility.Visible
						: Visibility.Collapsed;
					break;
				case "Three":
					vis = (cnt == ListCntEnums.Three) ? Visibility.Visible : Visibility.Collapsed;
					break;
			}
#endif
			return vis;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Visibility.Visible;
		}
	}
}