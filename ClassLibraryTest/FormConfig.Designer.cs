namespace QuickBox
{
	partial class FormConfig
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.chk_onLaunch = new System.Windows.Forms.CheckBox();
			this.chk_showVideo = new System.Windows.Forms.CheckBox();
			this.chk_speeedUpDecompress = new System.Windows.Forms.CheckBox();
			this.num_tailleCache = new System.Windows.Forms.NumericUpDown();
			this.num_delayShow = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chk_instantShow = new System.Windows.Forms.CheckBox();
			this.button1 = new System.Windows.Forms.Button();
			this.chk_muteVideo = new System.Windows.Forms.CheckBox();
			this.chk_showExtraInfo = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.num_tailleCache)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.num_delayShow)).BeginInit();
			this.SuspendLayout();
			// 
			// chk_onLaunch
			// 
			this.chk_onLaunch.AutoSize = true;
			this.chk_onLaunch.Location = new System.Drawing.Point(12, 12);
			this.chk_onLaunch.Name = "chk_onLaunch";
			this.chk_onLaunch.Size = new System.Drawing.Size(156, 17);
			this.chk_onLaunch.TabIndex = 0;
			this.chk_onLaunch.Text = "Show QuickBox on Launch";
			this.chk_onLaunch.UseVisualStyleBackColor = true;
			// 
			// chk_showVideo
			// 
			this.chk_showVideo.AutoSize = true;
			this.chk_showVideo.Location = new System.Drawing.Point(12, 35);
			this.chk_showVideo.Name = "chk_showVideo";
			this.chk_showVideo.Size = new System.Drawing.Size(83, 17);
			this.chk_showVideo.TabIndex = 2;
			this.chk_showVideo.Text = "Show Video";
			this.chk_showVideo.UseVisualStyleBackColor = true;
			// 
			// chk_speeedUpDecompress
			// 
			this.chk_speeedUpDecompress.AutoSize = true;
			this.chk_speeedUpDecompress.Location = new System.Drawing.Point(12, 58);
			this.chk_speeedUpDecompress.Name = "chk_speeedUpDecompress";
			this.chk_speeedUpDecompress.Size = new System.Drawing.Size(189, 17);
			this.chk_speeedUpDecompress.TabIndex = 3;
			this.chk_speeedUpDecompress.Text = "Speed Up Lanchbox Auto-Backup";
			this.chk_speeedUpDecompress.UseVisualStyleBackColor = true;
			// 
			// num_tailleCache
			// 
			this.num_tailleCache.Location = new System.Drawing.Point(118, 144);
			this.num_tailleCache.Name = "num_tailleCache";
			this.num_tailleCache.Size = new System.Drawing.Size(120, 20);
			this.num_tailleCache.TabIndex = 4;
			// 
			// num_delayShow
			// 
			this.num_delayShow.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.num_delayShow.Location = new System.Drawing.Point(118, 170);
			this.num_delayShow.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.num_delayShow.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.num_delayShow.Name = "num_delayShow";
			this.num_delayShow.Size = new System.Drawing.Size(120, 20);
			this.num_delayShow.TabIndex = 5;
			this.num_delayShow.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 151);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(77, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "PreCache Size";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(9, 177);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Delay Display";
			// 
			// chk_instantShow
			// 
			this.chk_instantShow.AutoSize = true;
			this.chk_instantShow.Location = new System.Drawing.Point(12, 207);
			this.chk_instantShow.Name = "chk_instantShow";
			this.chk_instantShow.Size = new System.Drawing.Size(111, 17);
			this.chk_instantShow.TabIndex = 8;
			this.chk_instantShow.Text = "Show Text instant";
			this.chk_instantShow.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(435, 248);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 10;
			this.button1.Text = "Save";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// chk_muteVideo
			// 
			this.chk_muteVideo.AutoSize = true;
			this.chk_muteVideo.Location = new System.Drawing.Point(10, 81);
			this.chk_muteVideo.Name = "chk_muteVideo";
			this.chk_muteVideo.Size = new System.Drawing.Size(79, 17);
			this.chk_muteVideo.TabIndex = 11;
			this.chk_muteVideo.Text = "Mute video";
			this.chk_muteVideo.UseVisualStyleBackColor = true;
			// 
			// chk_showExtraInfo
			// 
			this.chk_showExtraInfo.AutoSize = true;
			this.chk_showExtraInfo.Location = new System.Drawing.Point(10, 104);
			this.chk_showExtraInfo.Name = "chk_showExtraInfo";
			this.chk_showExtraInfo.Size = new System.Drawing.Size(98, 17);
			this.chk_showExtraInfo.TabIndex = 12;
			this.chk_showExtraInfo.Text = "Show ExtraInfo";
			this.chk_showExtraInfo.UseVisualStyleBackColor = true;
			// 
			// FormConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(522, 283);
			this.Controls.Add(this.chk_showExtraInfo);
			this.Controls.Add(this.chk_muteVideo);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.chk_instantShow);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.num_delayShow);
			this.Controls.Add(this.num_tailleCache);
			this.Controls.Add(this.chk_speeedUpDecompress);
			this.Controls.Add(this.chk_showVideo);
			this.Controls.Add(this.chk_onLaunch);
			this.Name = "FormConfig";
			this.Text = "FormConfig";
			this.Load += new System.EventHandler(this.FormConfig_Load);
			((System.ComponentModel.ISupportInitialize)(this.num_tailleCache)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.num_delayShow)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chk_onLaunch;
		private System.Windows.Forms.CheckBox chk_showVideo;
		private System.Windows.Forms.CheckBox chk_speeedUpDecompress;
		private System.Windows.Forms.NumericUpDown num_tailleCache;
		private System.Windows.Forms.NumericUpDown num_delayShow;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chk_instantShow;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.CheckBox chk_muteVideo;
		private System.Windows.Forms.CheckBox chk_showExtraInfo;
	}
}