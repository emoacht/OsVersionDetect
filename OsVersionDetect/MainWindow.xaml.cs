using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace OsVersionDetect
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			OsVersionByGetVersionEx = OsVersion.GetOsVersionByGetVersionEx();
			OsVersionByRtlGetVersion = OsVersion.GetOsVersionByRtlGetVersion();
			OsVersionByNetWkstaGetInfo = OsVersion.GetOsVersionByNetWkstaGetInfo();
			OsVersionByWmi = OsVersion.GetOsVersionByWmi();
			CheckOsEqualOrNewer();
		}


		#region GetVersionEx

		public Version OsVersionByGetVersionEx
		{
			get { return (Version)GetValue(OsVersionByGetVersionExProperty); }
			set { SetValue(OsVersionByGetVersionExProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByGetVersionExProperty =
			DependencyProperty.Register(
				"OsVersionByGetVersionEx",
				typeof(Version),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(null));

		#endregion


		#region RtlGetVersion

		public Version OsVersionByRtlGetVersion
		{
			get { return (Version)GetValue(OsVersionByRtlGetVersionProperty); }
			set { SetValue(OsVersionByRtlGetVersionProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByRtlGetVersionProperty =
			DependencyProperty.Register(
				"OsVersionByRtlGetVersion",
				typeof(Version),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(null));

		#endregion


		#region NetWkstaGetInfo

		public Version OsVersionByNetWkstaGetInfo
		{
			get { return (Version)GetValue(OsVersionByNetWkstaGetInfoProperty); }
			set { SetValue(OsVersionByNetWkstaGetInfoProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByNetWkstaGetInfoProperty =
			DependencyProperty.Register(
				"OsVersionByNetWkstaGetInfo",
				typeof(Version),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(null));

		#endregion


		#region WMI

		public Version OsVersionByWmi
		{
			get { return (Version)GetValue(OsVersionByWmiProperty); }
			set { SetValue(OsVersionByWmiProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByWmiProperty =
			DependencyProperty.Register(
				"OsVersionByWmi",
				typeof(Version),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(null));

		#endregion


		#region VerifyVersionInfo

		public int[] Numbers
		{
			get { return _numbers; }
		}
		private readonly int[] _numbers = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		public int MajorVersion
		{
			get { return (int)GetValue(MajorVersionProperty); }
			set { SetValue(MajorVersionProperty, value); }
		}
		public static readonly DependencyProperty MajorVersionProperty =
			DependencyProperty.Register(
				"MajorVersion",
				typeof(int),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(
					6,
					(d, e) => ((MainWindow)d).CheckOsEqualOrNewer()));

		public int MinorVersion
		{
			get { return (int)GetValue(MinorVersionProperty); }
			set { SetValue(MinorVersionProperty, value); }
		}
		public static readonly DependencyProperty MinorVersionProperty =
			DependencyProperty.Register(
				"MinorVersion",
				typeof(int),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(
					3,
					(d, e) => ((MainWindow)d).CheckOsEqualOrNewer()));

		public bool? IsOsEqualOrNewer
		{
			get { return (bool?)GetValue(IsOsEqualOrNewerProperty); }
			set { SetValue(IsOsEqualOrNewerProperty, value); }
		}
		public static readonly DependencyProperty IsOsEqualOrNewerProperty =
			DependencyProperty.Register(
				"IsOsEqualOrNewer",
				typeof(bool?),
				typeof(MainWindow),
				new FrameworkPropertyMetadata(false));

		private void CheckOsEqualOrNewer()
		{
			IsOsEqualOrNewer = OsVersion.IsOsEqualOrNewerByVerifyVersionInfo(MajorVersion, MinorVersion);
		}

		#endregion
	}
}