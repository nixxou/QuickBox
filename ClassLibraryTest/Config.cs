using System.IO;
using System.Reflection;

namespace QuickBox
{
	public class Config
	{
		private static string _pluginPath = "";

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
