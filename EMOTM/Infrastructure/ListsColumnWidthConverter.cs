using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
	internal class ListsColumnWidthConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			ListCntEnums cnt = (ListCntEnums) value;
			string whichCol = (string) parameter;
			GridLength width = new GridLength(1, GridUnitType.Star);
#if ZIGGY
            switch (cnt)
            {
                case ListCntEnums.One:
                    break;
                case ListCntEnums.Two:
                    break;
                case ListCntEnums.Three:
                    break;
            }
#else
			switch (whichCol)
			{
				case "One":
					width = new GridLength(1, GridUnitType.Star);
					break;
				case "Two":
					width = (cnt != ListCntEnums.One)
						? new GridLength(1, GridUnitType.Star)
						: new GridLength(0, GridUnitType.Star);
					break;
				case "Three":
					width = (cnt == ListCntEnums.Three)
						? new GridLength(1, GridUnitType.Star)
						: new GridLength(0, GridUnitType.Star);
					break;
			}
#endif
			return width;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return 1;
		}
	}
}