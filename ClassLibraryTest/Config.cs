using System.IO;
using System.Reflection;

namespace QuickBox
{
	public class Config
	{
		private static string _pluginPath = "";
		public static bool onLaunch = true;
		public static bool showVideo = true;
		public static int volumeVideo = 0;
		public static bool speeedUpDecompress = false;
		public static int tailleCache = 5;
		public static int delayShow = 200;
		public static bool instantShow = true;

		

		public static void LoadConfig()
		{
			string fileConfig = GetConfigFile();
			if (File.Exists(fileConfig))
			{
				IniFile ini = new IniFile(GetConfigFile());
				onLaunch = bool.Parse(ini.Read("QuickBox", "onLaunch", "True"));
				showVideo = bool.Parse(ini.Read("QuickBox", "ShowVideos", "True"));
				volumeVideo = int.Parse(ini.Read("QuickBox", "VolumeVideo", "0"));
				tailleCache = int.Parse(ini.Read("QuickBox", "tailleCache", "5"));
				delayShow = int.Parse(ini.Read("QuickBox", "delayShow", "200"));
				instantShow = bool.Parse(ini.Read("QuickBox", "instantShow", "True"));
			}
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));


			speeedUpDecompress = false;
			if (File.Exists(Path.Combine(launchBoxRootPath, "ThirdParty", "7-Zip", "7z-ori.exe")))
			{
				speeedUpDecompress = true;
			}

		}

		public static void SaveConfig()
		{
			string fileConfig = GetConfigFile();
			IniFile ini = new IniFile(GetConfigFile());
			ini.Write("QuickBox", "ShowVideos", onLaunch ? "True" : "False");
			ini.Write("QuickBox", "ShowVideos", showVideo ? "True" : "False");
			ini.Write("QuickBox", "VolumeVideo", volumeVideo.ToString());
			ini.Write("QuickBox", "tailleCache", tailleCache.ToString());
			ini.Write("QuickBox", "delayShow", delayShow.ToString());
			ini.Write("QuickBox", "instantShow", instantShow ? "True" : "False");

		}

		public static string GetPluginPath()
		{
			if (_pluginPath != "") return _pluginPath;
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\QuickBox";
			_pluginPath = Path.Combine(launchBoxRootPath, relativePluginPath);
			return _pluginPath;
		}

		public static string GetConfigFile()
		{
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyFileName = Path.GetFileName(assemblyPath);
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\QuickBox";

			string iniFilePath = Path.Combine(launchBoxRootPath, relativePluginPath, "config.ini");
			return iniFilePath;
		}

		public static string GetConfigBin()
		{
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyFileName = Path.GetFileName(assemblyPath);
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\QuickBox";

			string iniFilePath = Path.Combine(launchBoxRootPath, relativePluginPath, "config.bin");
			return iniFilePath;
		}

	}
}
