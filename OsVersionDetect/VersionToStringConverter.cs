using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace OsVersionDetect
{
	[ValueConversion(typeof(Version), typeof(string))]
	public class VersionToStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var version = value as Version;
			if (version == null)
				return DependencyProperty.UnsetValue;

			return version.ToString(2); // major, minor
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}