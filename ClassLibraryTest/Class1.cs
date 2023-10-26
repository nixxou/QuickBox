using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace QuickBox
{
	public class SystemEventsPlugin : ISystemEventsPlugin
	{


		public void OnEventRaised(string eventType)
		{
			if (eventType == "LaunchBoxStartupCompleted")
			{
				Config.LoadConfig();

				if (Config.onLaunch)
				{
					Config.LaunchboxWasShow = false;
					TimerCallback timerCallback = _ =>
					{
						var frm = new Form1();
						Application.Run(frm);
					};
					var timer = new System.Threading.Timer(timerCallback, null, 0, 1000);
				}
			}
		}

		private void LaunchForm()
		{
			var frm = new Form1();
			frm.Show();
		}
	}
}
