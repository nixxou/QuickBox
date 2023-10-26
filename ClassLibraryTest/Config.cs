using System.Drawing;
using System.IO;
using System.Reflection;

namespace QuickBox
{
	public class Config
	{
		private static string _pluginPath = "";
		public static bool onLaunch = true;
		public static bool showVideo = true;
		public static bool muteVideo = false;
		public static bool speeedUpDecompress = false;
		public static int tailleCache = 5;
		public static int delayShow = 200;
		public static bool instantShow = true;
		public static bool showExtraInfo = false;

		public static int SizeX = -1;
		public static int SizeY = -1;

		public static bool LaunchboxWasShow = true;




		public static void LoadConfig()
		{
			string fileConfig = GetConfigFile();
			if (File.Exists(fileConfig))
			{
				IniFile ini = new IniFile(GetConfigFile());
				onLaunch = bool.Parse(ini.Read("QuickBox", "onLaunch", "True"));
				showVideo = bool.Parse(ini.Read("QuickBox", "ShowVideos", "True"));
				muteVideo = bool.Parse(ini.Read("QuickBox", "MuteVideo", "False"));
				tailleCache = int.Parse(ini.Read("QuickBox", "tailleCache", "5"));
				delayShow = int.Parse(ini.Read("QuickBox", "delayShow", "200"));
				instantShow = bool.Parse(ini.Read("QuickBox", "instantShow", "True"));
				showExtraInfo = bool.Parse(ini.Read("QuickBox", "showExtraInfo", "False"));

				SizeX = int.Parse(ini.Read("QuickBox", "SizeX", "-1"));
				SizeY = int.Parse(ini.Read("QuickBox", "SizeY", "-1"));
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
			ini.Write("QuickBox", "onLaunch", onLaunch ? "True" : "False");
			ini.Write("QuickBox", "ShowVideos", showVideo ? "True" : "False");
			ini.Write("QuickBox", "MuteVideo", muteVideo ? "True" : "False");
			ini.Write("QuickBox", "tailleCache", tailleCache.ToString());
			ini.Write("QuickBox", "delayShow", delayShow.ToString());
			ini.Write("QuickBox", "instantShow", instantShow ? "True" : "False");
			ini.Write("QuickBox", "showExtraInfo", showExtraInfo ? "True" : "False");


		}

		public static void SaveSize(Size size)
		{
			SizeX = size.Width;
			SizeY = size.Height;
			string fileConfig = GetConfigFile();
			IniFile ini = new IniFile(GetConfigFile());
			ini.Write("QuickBox", "SizeX", SizeX.ToString());
			ini.Write("QuickBox", "SizeY", SizeY.ToString());
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
