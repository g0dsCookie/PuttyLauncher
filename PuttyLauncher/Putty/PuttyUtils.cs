using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace CookieProjects.PuttyLauncher.Putty
{
	public static class PuttyUtils
	{
		public static string RegistryBase => @"Software\SimonTatham\PuTTY";

		public static string RegistrySessions => RegistryBase + @"\Sessions";

		static string _PuttyPath = string.Empty;

		public static string PuttyPath
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_PuttyPath))
				{
					_PuttyPath = GetPath(@"PuTTY\putty.exe");
					if (_PuttyPath == @"PuTTY\putty.exe") _PuttyPath = "putty.exe";
				}
				return _PuttyPath;
			}
		}

		static string _PageAnt = string.Empty;

		public static string PageAnt
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_PageAnt))
				{
					_PageAnt = GetPath(@"PuTTY\pageant.exe");
					if (_PageAnt == @"PuTTY\pageant.exe") _PageAnt = "pageant.exe";
				}
				return _PageAnt;
			}
		}

		internal static string GetPath(string file)
		{
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), file);
			if (File.Exists(path)) return path;

			if (Environment.Is64BitOperatingSystem)
			{
				path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), file);
				if (File.Exists(path)) return path;
			}

			return file;
		}

		public static string[] GetSessions()
		{
			var sessionList = new List<string>();
			using (var baseKey = Registry.CurrentUser.OpenSubKey(RegistrySessions, false))
				foreach (var s in baseKey.GetSubKeyNames())
					if (!Config.GlobalConfig.IgnoreSessions.Contains(s))
						sessionList.Add(s);
			return sessionList.ToArray();
		}

		public static string[] GetPrivateKeys(string[] sessions)
		{
			var privateKeys = new List<string>();
			using (var baseKey = Registry.CurrentUser.OpenSubKey(RegistrySessions, false))
			{
				foreach (var s in sessions)
				{
					using (var subKey = baseKey.OpenSubKey(s))
					{
						var pKey = subKey.GetValue("PublicKeyFile", string.Empty).ToString();
						if (!privateKeys.Contains(pKey))
							privateKeys.Add(pKey);
					}
				}
			}
			return privateKeys.ToArray();
		}
	}
}
