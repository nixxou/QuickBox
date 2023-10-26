using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickBox
{
	public partial class FormConfig : Form
	{
		public FormConfig()
		{
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			Config.onLaunch = chk_onLaunch.Checked;
			Config.showVideo = chk_showVideo.Checked;
			Config.muteVideo = chk_muteVideo.Checked;
			Config.speeedUpDecompress = chk_speeedUpDecompress.Checked;
			Config.tailleCache = (int)num_tailleCache.Value;
			Config.delayShow = (int)num_delayShow.Value;
			Config.instantShow = chk_instantShow.Checked;
			Config.showExtraInfo = chk_showExtraInfo.Checked;
			Config.SaveConfig();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void FormConfig_Load(object sender, EventArgs e)
		{
			chk_onLaunch.Checked = Config.onLaunch;
			chk_showVideo.Checked = Config.showVideo;
			chk_muteVideo.Checked = Config.muteVideo;
			chk_speeedUpDecompress.Checked = Config.speeedUpDecompress;
			num_tailleCache.Value = Config.tailleCache;
			num_delayShow.Value = Config.delayShow;
			chk_instantShow.Checked = Config.instantShow;
			chk_showExtraInfo.Checked = Config.showExtraInfo;
		}
	}
}
