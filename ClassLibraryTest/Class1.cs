using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;

namespace QuickBox
{
	public class SystemEventsPlugin : ISystemEventsPlugin
	{


		public void OnEventRaised(string eventType)
		{
			if (eventType == "LaunchBoxStartupCompleted")
			{
				MessageBox.Show("start");
				var frm = new Form1();
				frm.Show();

			}
		}

		private void LaunchForm()
		{
			var frm = new Form1();
			frm.Show();
		}
	}
}
