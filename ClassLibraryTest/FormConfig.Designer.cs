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
			this.chk_EnableOnStart = new System.Windows.Forms.CheckBox();
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			this.SuspendLayout();
			// 
			// chk_EnableOnStart
			// 
			this.chk_EnableOnStart.AutoSize = true;
			this.chk_EnableOnStart.Location = new System.Drawing.Point(12, 12);
			this.chk_EnableOnStart.Name = "chk_EnableOnStart";
			this.chk_EnableOnStart.Size = new System.Drawing.Size(156, 17);
			this.chk_EnableOnStart.TabIndex = 0;
			this.chk_EnableOnStart.Text = "Show QuickBox on Launch";
			this.chk_EnableOnStart.UseVisualStyleBackColor = true;
			// 
			// trackBar1
			// 
			this.trackBar1.Location = new System.Drawing.Point(12, 58);
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(293, 45);
			this.trackBar1.TabIndex = 1;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 35);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(81, 17);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Play Videos";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// FormConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(317, 227);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.trackBar1);
			this.Controls.Add(this.chk_EnableOnStart);
			this.Name = "FormConfig";
			this.Text = "FormConfig";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chk_EnableOnStart;
		private System.Windows.Forms.TrackBar trackBar1;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}