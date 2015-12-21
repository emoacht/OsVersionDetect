using System;
using NUnit.Framework;

namespace OsVersionDetect.Test
{
	[TestFixture]
	public class VersionToStringConverterTest
	{
		private static readonly VersionToStringConverter _converter = new VersionToStringConverter();

		[TestCase("0.0", null)]
		[TestCase("", "-1")]
		[TestCase("", "0")]
		[TestCase("0", "1")]
		[TestCase("0.0", "2")]
		[TestCase("0.0", "3")]
		[TestCase("0.0", "4")]
		[TestCase("0.0", "5")]
		public void ConvertNull(string versionString, string fieldCount)
		{
			var version = new Version();

			Assert.AreEqual(versionString, (string)_converter.Convert(version, null, fieldCount, null));
		}

		[TestCase("1.2", null)]
		[TestCase("", "-1")]
		[TestCase("", "0")]
		[TestCase("1", "1")]
		[TestCase("1.2", "2")]
		[TestCase("1.2", "3")]
		[TestCase("1.2", "4")]
		[TestCase("1.2", "5")]
		public void ConvertMajorMinor(string versionString, string fieldCount)
		{
			var version = new Version(1, 2);

			Assert.AreEqual(versionString, (string)_converter.Convert(version, null, fieldCount, null));
		}

		[TestCase("1.2", null)]
		[TestCase("", "-1")]
		[TestCase("", "0")]
		[TestCase("1", "1")]
		[TestCase("1.2", "2")]
		[TestCase("1.2.3", "3")]
		[TestCase("1.2.3", "4")]
		[TestCase("1.2.3", "5")]
		public void ConvertMajorMinorBuild(string versionString, string fieldCount)
		{
			var version = new Version(1, 2, 3);

			Assert.AreEqual(versionString, (string)_converter.Convert(version, null, fieldCount, null));
		}

		[TestCase("1.2", null)]
		[TestCase("", "-1")]
		[TestCase("", "0")]
		[TestCase("1", "1")]
		[TestCase("1.2", "2")]
		[TestCase("1.2.3", "3")]
		[TestCase("1.2.3.4", "4")]
		[TestCase("1.2.3.4", "5")]
		public void ConvertMajorMinorBuildRevision(string versionString, string fieldCount)
		{
			var version = new Version(1, 2, 3, 4);

			Assert.AreEqual(versionString, (string)_converter.Convert(version, null, fieldCount, null));
		}
	}
}