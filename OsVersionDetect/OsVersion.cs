using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OsVersionDetect
{
	public static class OsVersion
	{
		#region Win32

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetVersionEx([In, Out] ref OSVERSIONINFOEX lpVersionInfo);

		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int RtlGetVersion([In, Out] ref OSVERSIONINFOEX lpVersionInformation);

		[StructLayout(LayoutKind.Sequential)]
		private struct OSVERSIONINFOEX
		{
			public uint dwOSVersionInfoSize;
			public uint dwMajorVersion;
			public uint dwMinorVersion;
			public uint dwBuildNumber;
			public uint dwPlatformId;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			public string szCSDVersion;
			public ushort wServicePackMajor;
			public ushort wServicePackMinor;
			public ushort wSuiteMask;
			public byte wProductType;
			public byte wReserved;
		}

		[DllImport("netapi32.dll", SetLastError = true)]
		private static extern int NetWkstaGetInfo(
			string servername,
			int level,
			out IntPtr bufptr);

		[StructLayout(LayoutKind.Sequential)]
		private struct WKSTA_INFO_100
		{
			public uint platform_id;
			public string computername;
			public string langroup;
			public uint ver_major;
			public uint ver_minor;
		}

		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool VerifyVersionInfoW(
			[In] ref OSVERSIONINFOEX lpVersionInfo,
			uint dwTypeMask,
			ulong dwlConditionMask);

		[DllImport("kernel32", SetLastError = true)]
		private static extern ulong VerSetConditionMask(
			ulong dwlConditionMask,
			uint dwTypeBitMask,
			byte dwConditionMask);

		private static uint VER_MINORVERSION = 0x0000001;
		private static uint VER_MAJORVERSION = 0x0000002;

		private static byte VER_GREATER_EQUAL = 3;

		#endregion


		public static Version GetOsVersionByGetVersionEx()
		{
			var info = new OSVERSIONINFOEX();
			info.dwOSVersionInfoSize = (uint)Marshal.SizeOf(info);

			var result = GetVersionEx(ref info);

			return result
				? new Version((int)info.dwMajorVersion, (int)info.dwMinorVersion)
				: null;
		}

		public static Version GetOsVersionByRtlGetVersion()
		{
			var info = new OSVERSIONINFOEX();
			info.dwOSVersionInfoSize = (uint)Marshal.SizeOf(info);

			var result = RtlGetVersion(ref info);

			return (result == 0) // STATUS_SUCCESS
				? new Version((int)info.dwMajorVersion, (int)info.dwMinorVersion)
				: null;
		}

		public static Version GetOsVersionByNetWkstaGetInfo()
		{
			IntPtr buff;

			var result = NetWkstaGetInfo(null, 100, out buff);

			if (result == 0) // NERR_Success
			{
				var info = (WKSTA_INFO_100)Marshal.PtrToStructure(buff, typeof(WKSTA_INFO_100));

				if (info.platform_id == 500) // PLATFORM_ID_NT
					return new Version((int)info.ver_major, (int)info.ver_minor);
			}

			return null;
		}

		public static Version GetOsVersionByWmi()
		{
			var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

			var os = searcher.Get().Cast<ManagementObject>().FirstOrDefault();

			if ((os != null) && (os["OsType"] != null) && (os["Version"] != null))
			{
				if (os["OsType"].ToString() == "18") // WINNT
					return new Version(os["Version"].ToString());
			}

			return null;
		}

		public static bool? IsOsEqualOrNewerByVerifyVersionInfo(int versionMajor, int versionMinor)
		{
			var info = new OSVERSIONINFOEX();
			info.dwMajorVersion = (uint)versionMajor;
			info.dwMinorVersion = (uint)versionMinor;
			info.dwOSVersionInfoSize = (uint)Marshal.SizeOf(info);

			ulong cm = 0;
			cm = VerSetConditionMask(cm, VER_MAJORVERSION, VER_GREATER_EQUAL);
			cm = VerSetConditionMask(cm, VER_MINORVERSION, VER_GREATER_EQUAL);

			var result = VerifyVersionInfoW(ref info, VER_MAJORVERSION | VER_MINORVERSION, cm);

			if (result)
				return true;

			return (Marshal.GetLastWin32Error() == 1150) // ERROR_OLD_WIN_VERSION
				? false
				: (bool?)null;
		}
	}
}