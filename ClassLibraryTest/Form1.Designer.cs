namespace QuickBox
{
	partial class Form1
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.MenuItem_Play = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_PlayVersion = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuItem_LaunchWith = new System.Windows.Forms.ToolStripMenuItem();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer_hideLb = new System.Windows.Forms.Timer(this.components);
			this.button2 = new System.Windows.Forms.Button();
			this.highlightTextRenderer1 = new BrightIdeasSoftware.HighlightTextRenderer();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.treeListView1 = new BrightIdeasSoftware.TreeListView();
			this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.fastObjectListView1 = new BrightIdeasSoftware.FastObjectListView();
			this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label_platform = new System.Windows.Forms.Label();
			this.label_gameTitle = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pictureBox_gameImage = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.flowLayoutPanelThumbs = new System.Windows.Forms.FlowLayoutPanel();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lbl_file = new System.Windows.Forms.Label();
			this.lbl_genre = new System.Windows.Forms.Label();
			this.lbl_publisher = new System.Windows.Forms.Label();
			this.lbl_developer = new System.Windows.Forms.Label();
			this.lbl_rlzdate = new System.Windows.Forms.Label();
			this.lbl_desc = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_gameImage)).BeginInit();
			this.panel2.SuspendLayout();
			this.flowLayoutPanelThumbs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Play,
            this.MenuItem_PlayVersion,
            this.MenuItem_LaunchWith});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(142, 70);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// MenuItem_Play
			// 
			this.MenuItem_Play.Name = "MenuItem_Play";
			this.MenuItem_Play.Size = new System.Drawing.Size(141, 22);
			this.MenuItem_Play.Text = "Play";
			this.MenuItem_Play.Click += new System.EventHandler(this.MenuItem_Play_Click);
			// 
			// MenuItem_PlayVersion
			// 
			this.MenuItem_PlayVersion.Name = "MenuItem_PlayVersion";
			this.MenuItem_PlayVersion.Size = new System.Drawing.Size(141, 22);
			this.MenuItem_PlayVersion.Text = "Play Version";
			// 
			// MenuItem_LaunchWith
			// 
			this.MenuItem_LaunchWith.Name = "MenuItem_LaunchWith";
			this.MenuItem_LaunchWith.Size = new System.Drawing.Size(141, 22);
			this.MenuItem_LaunchWith.Text = "Launch With";
			// 
			// timer_hideLb
			// 
			this.timer_hideLb.Interval = 20;
			this.timer_hideLb.Tick += new System.EventHandler(this.timer_hideLb_Tick);
			// 
			// button2
			// 
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.button2.Location = new System.Drawing.Point(215, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(117, 23);
			this.button2.TabIndex = 7;
			this.button2.Text = "Config";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.button1.Location = new System.Drawing.Point(40, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(102, 23);
			this.button1.TabIndex = 6;
			this.button1.Text = "Show Launchbox";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// textBox1
			// 
			this.textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.textBox1.Location = new System.Drawing.Point(3, 17);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(220, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.TextChanged += new System.EventHandler(this.TextBox1_TextChanged);
			// 
			// treeListView1
			// 
			this.treeListView1.AllColumns.Add(this.olvColumn4);
			this.treeListView1.CellEditUseWholeCell = false;
			this.treeListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn4});
			this.treeListView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.treeListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.treeListView1.HideSelection = false;
			this.treeListView1.Location = new System.Drawing.Point(3, 43);
			this.treeListView1.MultiSelect = false;
			this.treeListView1.Name = "treeListView1";
			this.treeListView1.ShowGroups = false;
			this.treeListView1.Size = new System.Drawing.Size(220, 976);
			this.treeListView1.TabIndex = 4;
			this.treeListView1.UseCompatibleStateImageBehavior = false;
			this.treeListView1.View = System.Windows.Forms.View.Details;
			this.treeListView1.VirtualMode = true;
			this.treeListView1.SelectedIndexChanged += new System.EventHandler(this.treeListView1_SelectedIndexChanged);
			// 
			// olvColumn4
			// 
			this.olvColumn4.AspectName = "NestedName";
			this.olvColumn4.FillsFreeSpace = true;
			// 
			// fastObjectListView1
			// 
			this.fastObjectListView1.AllColumns.Add(this.olvColumn1);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn2);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn3);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn5);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn6);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn7);
			this.fastObjectListView1.AllColumns.Add(this.olvColumn8);
			this.fastObjectListView1.CellEditUseWholeCell = false;
			this.fastObjectListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn8});
			this.fastObjectListView1.ContextMenuStrip = this.contextMenuStrip1;
			this.fastObjectListView1.Cursor = System.Windows.Forms.Cursors.Default;
			this.fastObjectListView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fastObjectListView1.FullRowSelect = true;
			this.fastObjectListView1.HideSelection = false;
			this.fastObjectListView1.Location = new System.Drawing.Point(229, 43);
			this.fastObjectListView1.MultiSelect = false;
			this.fastObjectListView1.Name = "fastObjectListView1";
			this.fastObjectListView1.ShowGroups = false;
			this.fastObjectListView1.Size = new System.Drawing.Size(901, 976);
			this.fastObjectListView1.TabIndex = 8;
			this.fastObjectListView1.UseCompatibleStateImageBehavior = false;
			this.fastObjectListView1.UseFilterIndicator = true;
			this.fastObjectListView1.UseFiltering = true;
			this.fastObjectListView1.View = System.Windows.Forms.View.Details;
			this.fastObjectListView1.VirtualMode = true;
			this.fastObjectListView1.SelectedIndexChanged += new System.EventHandler(this.fastObjectListView1_SelectedIndexChanged);
			// 
			// olvColumn1
			// 
			this.olvColumn1.AspectName = "Title";
			this.olvColumn1.Text = "Title";
			this.olvColumn1.Width = 369;
			// 
			// olvColumn2
			// 
			this.olvColumn2.AspectName = "Platform";
			this.olvColumn2.Text = "Platform";
			this.olvColumn2.Width = 180;
			// 
			// olvColumn3
			// 
			this.olvColumn3.AspectName = "Developer";
			this.olvColumn3.Text = "Developer";
			this.olvColumn3.Width = 100;
			// 
			// olvColumn5
			// 
			this.olvColumn5.AspectName = "Publisher";
			this.olvColumn5.Text = "Publisher";
			this.olvColumn5.Width = 100;
			// 
			// olvColumn6
			// 
			this.olvColumn6.AspectName = "ApplicationPath";
			this.olvColumn6.Text = "ApplicationPath";
			this.olvColumn6.Width = 250;
			// 
			// olvColumn7
			// 
			this.olvColumn7.AspectName = "ReleaseDate";
			this.olvColumn7.Text = "ReleaseDate";
			// 
			// olvColumn8
			// 
			this.olvColumn8.AspectName = "Rating";
			this.olvColumn8.Text = "Rating";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 370F));
			this.tableLayoutPanel1.Controls.Add(this.treeListView1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.textBox1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.fastObjectListView1, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1504, 1022);
			this.tableLayoutPanel1.TabIndex = 10;
			this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint_1);
			// 
			// tableLayoutPanel3
			// 
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel3.Controls.Add(this.button1, 0, 0);
			this.tableLayoutPanel3.Controls.Add(this.button2, 1, 0);
			this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel3.Location = new System.Drawing.Point(1136, 3);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
			this.tableLayoutPanel3.Size = new System.Drawing.Size(365, 34);
			this.tableLayoutPanel3.TabIndex = 1;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoScroll = true;
			this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
			this.flowLayoutPanel1.Controls.Add(this.label_platform);
			this.flowLayoutPanel1.Controls.Add(this.label_gameTitle);
			this.flowLayoutPanel1.Controls.Add(this.panel1);
			this.flowLayoutPanel1.Controls.Add(this.panel2);
			this.flowLayoutPanel1.Controls.Add(this.panel3);
			this.flowLayoutPanel1.Controls.Add(this.lbl_desc);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(1136, 43);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
			this.flowLayoutPanel1.Size = new System.Drawing.Size(365, 976);
			this.flowLayoutPanel1.TabIndex = 9;
			this.flowLayoutPanel1.WrapContents = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pictureBox1.Location = new System.Drawing.Point(15, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.pictureBox1.Size = new System.Drawing.Size(320, 180);
			this.pictureBox1.TabIndex = 5;
			this.pictureBox1.TabStop = false;
			// 
			// label_platform
			// 
			this.label_platform.AutoSize = true;
			this.label_platform.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_platform.ForeColor = System.Drawing.Color.Gray;
			this.label_platform.Location = new System.Drawing.Point(3, 186);
			this.label_platform.Name = "label_platform";
			this.label_platform.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.label_platform.Size = new System.Drawing.Size(310, 39);
			this.label_platform.TabIndex = 3;
			this.label_platform.Text = "SUPER NINTENDO ENTERTAINMENT SYSTEM";
			// 
			// label_gameTitle
			// 
			this.label_gameTitle.AutoSize = true;
			this.label_gameTitle.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_gameTitle.Location = new System.Drawing.Point(3, 225);
			this.label_gameTitle.MaximumSize = new System.Drawing.Size(350, 0);
			this.label_gameTitle.Name = "label_gameTitle";
			this.label_gameTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
			this.label_gameTitle.Size = new System.Drawing.Size(119, 36);
			this.label_gameTitle.TabIndex = 6;
			this.label_gameTitle.Text = "TITRE GAME";
			// 
			// panel1
			// 
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panel1.Controls.Add(this.pictureBox_gameImage);
			this.panel1.Location = new System.Drawing.Point(3, 264);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(345, 183);
			this.panel1.TabIndex = 9;
			// 
			// pictureBox_gameImage
			// 
			this.pictureBox_gameImage.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.pictureBox_gameImage.Location = new System.Drawing.Point(12, 0);
			this.pictureBox_gameImage.Name = "pictureBox_gameImage";
			this.pictureBox_gameImage.Size = new System.Drawing.Size(320, 180);
			this.pictureBox_gameImage.TabIndex = 9;
			this.pictureBox_gameImage.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.flowLayoutPanelThumbs);
			this.panel2.Location = new System.Drawing.Point(3, 453);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(339, 92);
			this.panel2.TabIndex = 11;
			// 
			// flowLayoutPanelThumbs
			// 
			this.flowLayoutPanelThumbs.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.flowLayoutPanelThumbs.AutoScroll = true;
			this.flowLayoutPanelThumbs.Controls.Add(this.pictureBox2);
			this.flowLayoutPanelThumbs.Location = new System.Drawing.Point(0, 0);
			this.flowLayoutPanelThumbs.Name = "flowLayoutPanelThumbs";
			this.flowLayoutPanelThumbs.Size = new System.Drawing.Size(339, 89);
			this.flowLayoutPanelThumbs.TabIndex = 11;
			this.flowLayoutPanelThumbs.WrapContents = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(76, 77);
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.panel3.Controls.Add(this.lbl_file);
			this.panel3.Controls.Add(this.lbl_genre);
			this.panel3.Controls.Add(this.lbl_publisher);
			this.panel3.Controls.Add(this.lbl_developer);
			this.panel3.Controls.Add(this.lbl_rlzdate);
			this.panel3.Location = new System.Drawing.Point(6, 551);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(339, 120);
			this.panel3.TabIndex = 12;
			this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
			// 
			// lbl_file
			// 
			this.lbl_file.AutoSize = true;
			this.lbl_file.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_file.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_file.Location = new System.Drawing.Point(0, 81);
			this.lbl_file.MaximumSize = new System.Drawing.Size(350, 0);
			this.lbl_file.Name = "lbl_file";
			this.lbl_file.Size = new System.Drawing.Size(40, 19);
			this.lbl_file.TabIndex = 16;
			this.lbl_file.Text = "File :";
			this.lbl_file.Click += new System.EventHandler(this.label5_Click);
			// 
			// lbl_genre
			// 
			this.lbl_genre.AutoSize = true;
			this.lbl_genre.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_genre.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_genre.Location = new System.Drawing.Point(0, 62);
			this.lbl_genre.MaximumSize = new System.Drawing.Size(350, 0);
			this.lbl_genre.Name = "lbl_genre";
			this.lbl_genre.Size = new System.Drawing.Size(56, 19);
			this.lbl_genre.TabIndex = 15;
			this.lbl_genre.Text = "Genre :";
			// 
			// lbl_publisher
			// 
			this.lbl_publisher.AutoSize = true;
			this.lbl_publisher.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_publisher.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_publisher.Location = new System.Drawing.Point(0, 43);
			this.lbl_publisher.MaximumSize = new System.Drawing.Size(350, 0);
			this.lbl_publisher.Name = "lbl_publisher";
			this.lbl_publisher.Size = new System.Drawing.Size(81, 19);
			this.lbl_publisher.TabIndex = 14;
			this.lbl_publisher.Text = "Publisher : ";
			// 
			// lbl_developer
			// 
			this.lbl_developer.AutoSize = true;
			this.lbl_developer.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_developer.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_developer.Location = new System.Drawing.Point(0, 24);
			this.lbl_developer.MaximumSize = new System.Drawing.Size(350, 0);
			this.lbl_developer.Name = "lbl_developer";
			this.lbl_developer.Size = new System.Drawing.Size(87, 19);
			this.lbl_developer.TabIndex = 13;
			this.lbl_developer.Text = "Developer : ";
			// 
			// lbl_rlzdate
			// 
			this.lbl_rlzdate.AutoSize = true;
			this.lbl_rlzdate.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_rlzdate.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_rlzdate.Location = new System.Drawing.Point(0, 0);
			this.lbl_rlzdate.MaximumSize = new System.Drawing.Size(350, 0);
			this.lbl_rlzdate.Name = "lbl_rlzdate";
			this.lbl_rlzdate.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.lbl_rlzdate.Size = new System.Drawing.Size(108, 24);
			this.lbl_rlzdate.TabIndex = 12;
			this.lbl_rlzdate.Text = "Release Date : ";
			// 
			// lbl_desc
			// 
			this.lbl_desc.AutoSize = true;
			this.lbl_desc.Dock = System.Windows.Forms.DockStyle.Top;
			this.lbl_desc.Font = new System.Drawing.Font("Calibri Light", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbl_desc.Location = new System.Drawing.Point(3, 674);
			this.lbl_desc.MaximumSize = new System.Drawing.Size(320, 0);
			this.lbl_desc.Name = "lbl_desc";
			this.lbl_desc.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
			this.lbl_desc.Size = new System.Drawing.Size(320, 18);
			this.lbl_desc.TabIndex = 15;
			this.lbl_desc.Text = "xxxx";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1504, 1022);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.treeListView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fastObjectListView1)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox_gameImage)).EndInit();
			this.panel2.ResumeLayout(false);
			this.flowLayoutPanelThumbs.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Timer timer_hideLb;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_Play;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_PlayVersion;
		private System.Windows.Forms.ToolStripMenuItem MenuItem_LaunchWith;
		private System.Windows.Forms.Button button2;
		private BrightIdeasSoftware.HighlightTextRenderer highlightTextRenderer1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textBox1;
		private BrightIdeasSoftware.TreeListView treeListView1;
		private BrightIdeasSoftware.OLVColumn olvColumn4;
		private BrightIdeasSoftware.FastObjectListView fastObjectListView1;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private BrightIdeasSoftware.OLVColumn olvColumn2;
		private BrightIdeasSoftware.OLVColumn olvColumn3;
		private BrightIdeasSoftware.OLVColumn olvColumn5;
		private BrightIdeasSoftware.OLVColumn olvColumn6;
		private BrightIdeasSoftware.OLVColumn olvColumn7;
		private BrightIdeasSoftware.OLVColumn olvColumn8;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label_platform;
		private System.Windows.Forms.Label label_gameTitle;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox_gameImage;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelThumbs;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label lbl_file;
		private System.Windows.Forms.Label lbl_genre;
		private System.Windows.Forms.Label lbl_publisher;
		private System.Windows.Forms.Label lbl_developer;
		private System.Windows.Forms.Label lbl_rlzdate;
		private System.Windows.Forms.Label lbl_desc;
	}
}