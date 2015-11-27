using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace OsVersionDetect
{
	public static class OsVersionAnother
	{
		#region Registry

		public static string CurrentVersion => GetRegistryValue(nameof(CurrentVersion));
		public static string CurrentBuild => GetRegistryValue(nameof(CurrentBuild));
		public static string BuildLab => GetRegistryValue(nameof(BuildLab));
		public static string ReleaseId => GetRegistryValue(nameof(ReleaseId));

		private static string GetRegistryValue(string name)
		{
			return _currentVersionRegistryValues.Value.ContainsKey(name)
				? _currentVersionRegistryValues.Value[name]
				: null;
		}

		private static Lazy<Dictionary<string, string>> _currentVersionRegistryValues =
			new Lazy<Dictionary<string, string>>(() => GetCurrentVersionRegistryValues() ?? new Dictionary<string, string>());

		private static Dictionary<string, string> GetCurrentVersionRegistryValues()
		{
			using (var localMachine = Registry.LocalMachine)
			using (var currentVersion = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion"))
			{
				return currentVersion?.GetValueNames()
					.ToDictionary(x => x, x => currentVersion.GetValue(x)?.ToString());
			}
		}

		#endregion

		#region System file

		public static string GetMsInfoFileVersion() => GetSystemFileVersion("msinfo32.exe");

		private static string GetSystemFileVersion(string fileName)
		{
			var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), fileName);

			if (!File.Exists(filePath))
				return null;

			return FileVersionInfo.GetVersionInfo(filePath).FileVersion;
		}

		#endregion
	}
}