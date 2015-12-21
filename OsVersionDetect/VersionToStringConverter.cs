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

			int fieldCount;
			if (!int.TryParse(parameter as string, out fieldCount))
				fieldCount = 2; // major.minor

			fieldCount = CoerceFieldCount(version, fieldCount);

			return version.ToString(fieldCount);
		}

		private static int CoerceFieldCount(Version version, int fieldCount)
		{
			if (fieldCount <= 0)
				return 0;

			for (int i = 1; i <= fieldCount; i++)
			{
				switch (i)
				{
					case 1:
						if (version.Major < 0)
							return 0;

						break;
					case 2:
						if (version.Minor < 0)
							return 1;

						break;
					case 3:
						if (version.Build < 0)
							return 2;

						break;
					default:
						if (version.Revision < 0)
							return 3;

						return 4;
				}
			}

			return fieldCount;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}