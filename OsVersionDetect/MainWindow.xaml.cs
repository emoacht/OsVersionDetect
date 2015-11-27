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

			OsVersionByCurrentVersion = OsVersionAnother.CurrentVersion;
			OsBuildNumberByCurrentBuild = OsVersionAnother.CurrentBuild;
			OsBuildLab = OsVersionAnother.BuildLab;
			OsReleaseId = OsVersionAnother.ReleaseId;
		}

		#region GetVersionEx

		public Version OsVersionByGetVersionEx
		{
			get { return (Version)GetValue(OsVersionByGetVersionExProperty); }
			set { SetValue(OsVersionByGetVersionExProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByGetVersionExProperty =
			DependencyProperty.Register(
				nameof(OsVersionByGetVersionEx),
				typeof(Version),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region RtlGetVersion

		public Version OsVersionByRtlGetVersion
		{
			get { return (Version)GetValue(OsVersionByRtlGetVersionProperty); }
			set { SetValue(OsVersionByRtlGetVersionProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByRtlGetVersionProperty =
			DependencyProperty.Register(
				nameof(OsVersionByRtlGetVersion),
				typeof(Version),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region NetWkstaGetInfo

		public Version OsVersionByNetWkstaGetInfo
		{
			get { return (Version)GetValue(OsVersionByNetWkstaGetInfoProperty); }
			set { SetValue(OsVersionByNetWkstaGetInfoProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByNetWkstaGetInfoProperty =
			DependencyProperty.Register(
				nameof(OsVersionByNetWkstaGetInfo),
				typeof(Version),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region WMI

		public Version OsVersionByWmi
		{
			get { return (Version)GetValue(OsVersionByWmiProperty); }
			set { SetValue(OsVersionByWmiProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByWmiProperty =
			DependencyProperty.Register(
				nameof(OsVersionByWmi),
				typeof(Version),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region VerifyVersionInfo

		public int[] Numbers { get; } = Enumerable.Range(0, 11).ToArray();

		public int MajorVersion
		{
			get { return (int)GetValue(MajorVersionProperty); }
			set { SetValue(MajorVersionProperty, value); }
		}
		public static readonly DependencyProperty MajorVersionProperty =
			DependencyProperty.Register(
				nameof(MajorVersion),
				typeof(int),
				typeof(MainWindow),
				new PropertyMetadata(
					6,
					(d, e) => ((MainWindow)d).CheckOsEqualOrNewer()));

		public int MinorVersion
		{
			get { return (int)GetValue(MinorVersionProperty); }
			set { SetValue(MinorVersionProperty, value); }
		}
		public static readonly DependencyProperty MinorVersionProperty =
			DependencyProperty.Register(
				nameof(MinorVersion),
				typeof(int),
				typeof(MainWindow),
				new PropertyMetadata(
					3,
					(d, e) => ((MainWindow)d).CheckOsEqualOrNewer()));

		public bool? IsOsEqualOrNewer
		{
			get { return (bool?)GetValue(IsOsEqualOrNewerProperty); }
			set { SetValue(IsOsEqualOrNewerProperty, value); }
		}
		public static readonly DependencyProperty IsOsEqualOrNewerProperty =
			DependencyProperty.Register(
				nameof(IsOsEqualOrNewer),
				typeof(bool?),
				typeof(MainWindow),
				new PropertyMetadata(false));

		private void CheckOsEqualOrNewer()
		{
			IsOsEqualOrNewer = OsVersion.IsOsEqualOrNewerByVerifyVersionInfo(MajorVersion, MinorVersion);
		}

		#endregion

		#region CurrentVersion

		public string OsVersionByCurrentVersion
		{
			get { return (string)GetValue(OsVersionByCurrentVersionProperty); }
			set { SetValue(OsVersionByCurrentVersionProperty, value); }
		}
		public static readonly DependencyProperty OsVersionByCurrentVersionProperty =
			DependencyProperty.Register(
				nameof(OsVersionByCurrentVersion),
				typeof(string),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region CurrentBuild

		public string OsBuildNumberByCurrentBuild
		{
			get { return (string)GetValue(OsBuildNumberByCurrentBuildProperty); }
			set { SetValue(OsBuildNumberByCurrentBuildProperty, value); }
		}
		public static readonly DependencyProperty OsBuildNumberByCurrentBuildProperty =
			DependencyProperty.Register(
				nameof(OsBuildNumberByCurrentBuild),
				typeof(string),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region BuildLab

		public string OsBuildLab
		{
			get { return (string)GetValue(OsBuildLabProperty); }
			set { SetValue(OsBuildLabProperty, value); }
		}
		public static readonly DependencyProperty OsBuildLabProperty =
			DependencyProperty.Register(
				nameof(OsBuildLab),
				typeof(string),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion

		#region ReleaseId

		public string OsReleaseId
		{
			get { return (string)GetValue(OsReleaseIdProperty); }
			set { SetValue(OsReleaseIdProperty, value); }
		}
		public static readonly DependencyProperty OsReleaseIdProperty =
			DependencyProperty.Register(
				nameof(OsReleaseId),
				typeof(string),
				typeof(MainWindow),
				new PropertyMetadata(null));

		#endregion
	}
}