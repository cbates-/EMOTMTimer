using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace EMOTM.Infrastructure
{
	public class WatermarkTextConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string watermark = "List work to do this minute";
			string thisName = (string) value;

			int indx = thisName.IndexOf("Minute", StringComparison.Ordinal);
			watermark = string.Format("List the work{1}to do {0} minute", thisName.Substring(0, indx), Environment.NewLine);


			return watermark;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}