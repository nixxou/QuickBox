using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins.RetroAchievements;
using Vlc.DotNet.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

namespace QuickBox
{
	public partial class Form1 : Form
	{
		const int SW_HIDE = 0;
		const int SW_SHOW = 5;

		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		[DllImport("user32.dll")]
		public static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);


		private bool texteModifie = false;
		private Dictionary<string, IGame[]> preloadGames = new Dictionary<string, IGame[]>();
		private Dictionary<string, object> preloadPlatformCategory = new Dictionary<string, object>();
		private Dictionary<string, Image> preloadPlatformIcons = new Dictionary<string, Image>();

		private System.Windows.Forms.Timer imageLoadTimer = new System.Windows.Forms.Timer();
		private System.Windows.Forms.Timer platformLoadTimer = new System.Windows.Forms.Timer();

		private IGame selectedGame = null;
		private int selectedIndex = -1;
		private IPlatform selectedPlatform = null;
		/*
		private string selectedClearLogoPath = "";
		private string selectedVideoPath = "";
		private string selectedBackgroundPath = "";
		private string selectedMainImage = "";
		private ImageDetails[] selectedImgList = null;
		*/

		private Dictionary<int,CacheImg> cache = new Dictionary<int,CacheImg>(Config.tailleCache * 2);

		private Dictionary<int, CacheImg> newcache = new Dictionary<int, CacheImg>(Config.tailleCache * 2);
		private int cacheIndexMin = -1;
		private int cacheIndexMax = -1;
		private Task TaskGenerateCache = null;


		private bool isGeneratingCache = false;
		bool forceCacheReset = false;

		private bool launchboxHidden = false;
		private Vlc.DotNet.Forms.VlcControl vlcControl = null;

		private bool tempLoad = false;

		private Image LoadingImg;
		private Image BackgroundEmpty;

		private bool _gameLaunched = false;
		private System.Threading.Timer _resetGameLaunchedTimer;

		public bool gameLaunched
		{
			get
			{
				return this._gameLaunched;
			}
			set
			{
				if (value == true)
				{
					this._gameLaunched = true;
					if (_resetGameLaunchedTimer != null) _resetGameLaunchedTimer.Dispose();
					
					_resetGameLaunchedTimer = new System.Threading.Timer(_ =>
					{
						this._gameLaunched = false;
						_resetGameLaunchedTimer.Dispose(); // N'oubliez pas de libérer les ressources du timer
					}, null, 5000, Timeout.Infinite);
				}
				else
				{
					this._gameLaunched = false;
				}
			}
		}





		public Form1()
		{

			Config.LoadConfig();
			var rootPlatforms = PluginHelper.DataManager.GetRootPlatformsCategoriesPlaylists();
			foreach(var p in rootPlatforms)
			{
				LoadPlatform(p);
			}
			Task.Run(() => PreloadPlatformName());



			InitializeComponent();
			treeListView2.BackColor = this.BackColor;
			treeListView2.BorderStyle = BorderStyle.None;
			//treeListView2.BorderStyle = BorderStyle.FixedSingle;
			treeListView2.HyperlinkClicked += HyperLinkClicked;
			treeListView2.Visible = false;
			treeListView2.Scrollable = false;

			ClearDisplay();
			Image background = System.Drawing.Image.FromFile(Path.Combine(Config.GetPluginPath(), "background.jpg"));
			BackgroundEmpty = ResizeImage(background, pictureBox1.Size);

			LoadingImg = GenerateLogo("", "", "Loading...", pictureBox1.Size);

			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);
			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string vlcLibPath = Path.Combine(launchBoxRootPath, "Core", "ThirdParty", "VLC", "x64");

			var libDirectory = new DirectoryInfo(vlcLibPath);

			var options = new string[]
			{
                // VLC options can be given here. Please refer to the VLC command line documentation.
            };


			vlcControl = new Vlc.DotNet.Forms.VlcControl();
			vlcControl.BeginInit();
			vlcControl.VlcLibDirectory = libDirectory;
			if(Config.muteVideo) vlcControl.VlcMediaplayerOptions = new string[] { "--aout=directsound", "--no-audio" };
			else vlcControl.VlcMediaplayerOptions = new string[] { "--aout=directsound" };

			vlcControl.Location = pictureBox_gameImage.Location;
			vlcControl.Size = pictureBox_gameImage.Size;
			pictureBox_gameImage.Parent.Controls.Add(vlcControl);
			//fake_textbox.Parent.Controls.Remove(fake_textbox);
			vlcControl.EndInit();
			vlcControl.Visible = false;
			pictureBox_gameImage.Visible = false;
			pictureBox1.BackColor = Color.Transparent;

			InitializeListView();
			this.MinimumSize = this.Size;
			imageLoadTimer.Interval = Config.delayShow;
			imageLoadTimer.Tick += ImageLoadTimer_Tick;

			platformLoadTimer.Interval = Config.delayShow;
			platformLoadTimer.Tick += PlatformLoadTimer_Tick;


			textBox1.TextChanged += TextBox1_TextChanged;
			

			// Configurez le minuteur pour déclencher l'appel à filter après 500 ms
			timer1.Interval = 500; // Définissez l'intervalle du minuteur à 500 ms
			timer1.Tick += Timer1_Tick; // Abonnez-vous à l'événement Tick du minuteur
			timer1.Start(); // Démarrez le minuteur


			var Games = PluginHelper.DataManager.GetAllGames();

            this.fastObjectListView1.SetObjects(Games);
			fastObjectListView1.ItemActivate += new System.EventHandler(this.fastObjectListView1_ItemActivate);

			this.treeListView1.CanExpandGetter = delegate (object x) {
				var p = (IPlatform)x;
				return p.GetChildren().Any();
			};
			this.treeListView1.ChildrenGetter = delegate (object x) {
				var p = (IPlatform)x;
				return p.GetChildren();
			};

			treeListView1.Roots = rootPlatforms;
			fastObjectListView1.AfterSorting += CleanCache;

			if (Config.SizeX >= this.Size.Width && Config.SizeY >= this.Size.Height)
			{
				this.Size = new Size(Config.SizeX, Config.SizeY);
			}
		}

		private void CleanCache(object sender, EventArgs e)
		{
			try
			{
				if (isGeneratingCache) forceCacheReset = true;
				cache.Clear();
			}
			catch { }
		}

		private void PlatformLoadTimer_Tick(object sender, EventArgs e)
		{
			if(selectedPlatform != null)
			{
				Task.Run(() => PlateformDisplay(selectedPlatform, true));
			}
			platformLoadTimer.Stop();
		}

		private void HyperLinkClicked(object sender, HyperlinkClickedEventArgs e)
		{
			e.Handled = true;
			Uri uri = new Uri(e.Url);
			Process.Start(new ProcessStartInfo(uri.ToString()) { UseShellExecute = true });
		}

		private void InitializeListView()
		{
			//olvColumn4
			this.olvColumn4.ImageGetter = delegate (object rowObject)
			{
				IPlatform p = (IPlatform)rowObject;
				if(preloadPlatformIcons.ContainsKey(p.Name)) return preloadPlatformIcons[p.Name];
				return null;
			};

			this.olvColumn9.AspectToStringConverter = delegate (object x)
			{
				string path = (string)x;
				string result = "";
				if (!string.IsNullOrEmpty(path))
				{
					try
					{
						result = Path.GetFileName(path);
					}
					catch { }
				}
				return result;
			};

			this.olvColumn7.AspectToStringConverter = delegate (object x)
			{
				if (x != null)
				{
					DateTime dateTime = (DateTime)x;
					return dateTime.ToShortDateString();
				}
				return string.Empty;
			};

			this.olvColumn17.AspectToStringConverter = delegate (object x)
			{
				if (x != null)
				{
					DateTime dateTime = (DateTime)x;
					return dateTime.ToShortDateString();
				}
				return string.Empty;
			};

			this.olvColumn18.AspectToStringConverter = delegate (object x)
			{
				if (x != null)
				{
					DateTime dateTime = (DateTime)x;
					return dateTime.ToShortDateString();
				}
				return string.Empty;
			};

			this.olvColumn20.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn21.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn22.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn23.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn24.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn29.AspectGetter = delegate (object x)
			{
				if (x == null) return "";
				var game = (IGame)x;
				string result = "";
				foreach (IAlternateName name in game.GetAllAlternateNames())
				{
					result += name.Name + ", ";
				}
				result = result.Trim();
				result = result.Trim(',');
				result = result.Trim();
				return result;
			};

			this.olvColumn34.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return (bool)x ? "Yes" : "No";
			};

			this.olvColumn36.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				TimeSpan temps = TimeSpan.FromSeconds((int)x);
				return temps.ToString(@"hh\:mm\:ss");
			};
			this.olvColumn27.AspectToStringConverter = delegate (object x)
			{
				if (x == null) return "";
				return Math.Round((System.Single)x, 2).ToString();
			};
			

			contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
			this.fastObjectListView1.Activation = System.Windows.Forms.ItemActivation.Standard;
			this.fastObjectListView1.HotTracking = false;
			this.fastObjectListView1.HoverSelection = false;

			try
			{
				string fileName = Config.GetConfigBin();
				if (File.Exists(fileName))
				{
					byte[] restoredData;
					using (FileStream fs = new FileStream(fileName, FileMode.Open))
					{
						restoredData = new byte[fs.Length];
						fs.Read(restoredData, 0, (int)fs.Length);
					}
					this.fastObjectListView1.RestoreState(restoredData);
				}

			}
			catch { }



		}

		private void contextMenuStrip1_Opened(object sender, EventArgs e)
		{
			if (fastObjectListView1.SelectedIndex < 0)
			{
				MenuItem_Play.Enabled = false;
				MenuItem_PlayVersion.Visible = false;
				MenuItem_LaunchWith.Visible = false;
			}
			else
			{
				MenuItem_Play.Enabled = true;
			}

			MenuItem_PlayVersion.Visible = false;
			MenuItem_LaunchWith.Visible = false;
			MenuItem_LaunchWith.DropDownItems.Clear();
			MenuItem_PlayVersion.DropDownItems.Clear();


			if (fastObjectListView1.SelectedIndex >= 0)
			{
				IGame game = (IGame)this.fastObjectListView1.SelectedObject;

				MenuItem_Play.Visible = true;
				
				var Emulators = PluginHelper.DataManager.GetAllEmulators()
					.Where(emulator => emulator.GetAllEmulatorPlatforms()
						.Where(ep => ep.Platform == game.Platform).Any());
				if (Emulators.Any())
				{
					MenuItem_LaunchWith.Visible = true;

					var DefaultEmulator = PluginHelper.DataManager.GetAllEmulators()
					.Where(emulator => emulator.GetAllEmulatorPlatforms()
						.Where(ep => ep.Platform == game.Platform && ep.IsDefault).Count() > 0).FirstOrDefault();
					
					foreach (var emulator in Emulators)
					{
						var subMenuItem1 = new ToolStripMenuItem($"Launch with {emulator.Title}");
						subMenuItem1.Click += (senderemu, eemu) =>
						{
							if(vlcControl != null && vlcControl.Visible) { vlcControl.Stop(); }
							gameLaunched = true;
							PluginHelper.LaunchBoxMainViewModel.PlayGame(game, null, emulator, null);
						};
						MenuItem_LaunchWith.DropDownItems.Add(subMenuItem1);
					}

					var addApps = game.GetAllAdditionalApplications();
					if (addApps.Any())
					{
						MenuItem_PlayVersion.Visible = true;
						foreach (var addApp in addApps)
						{
							var subMenuItem1 = new ToolStripMenuItem($"Play Version {addApp.Name}");
							subMenuItem1.Click += (senderemu, eemu) =>
							{
								if (vlcControl != null && vlcControl.Visible) { vlcControl.Stop(); }
								gameLaunched = true;
								PluginHelper.LaunchBoxMainViewModel.PlayGame(game, addApp, DefaultEmulator, null);
							};
							MenuItem_PlayVersion.DropDownItems.Add(subMenuItem1);
						}
					}
				}

			}


		}

		private void MenuItem_Play_Click(object sender, EventArgs e)
		{
			if (fastObjectListView1.SelectedIndex >= 0)
			{
				IGame game = (IGame)this.fastObjectListView1.SelectedObject;

				var Emulator = PluginHelper.DataManager.GetAllEmulators()
					.Where(emulator => emulator.GetAllEmulatorPlatforms()
						.Where(ep => ep.Platform == game.Platform && ep.IsDefault).Count() > 0).FirstOrDefault();

				if (vlcControl != null && vlcControl.Visible) { vlcControl.Stop(); }
				gameLaunched = true;
				PluginHelper.LaunchBoxMainViewModel.PlayGame(game, null, Emulator, null);
			}
		}

		private Image GetPlatformIcon(IPlatform p)
		{
			string original = p.ClearLogoImagePath;
			if (!string.IsNullOrEmpty(original))
			{
				string FileLogo = Path.GetFileName(original);
				string ParentDir1 = Path.GetDirectoryName(original);
				string ParentDir2 = Path.GetDirectoryName(ParentDir1);
				string Part1 = Path.GetFileName(ParentDir2);
				string ParentDir3 = Path.GetDirectoryName(ParentDir2);
				string Part2 = Path.GetFileName(ParentDir3);
				string ParentDir4 = Path.GetDirectoryName(ParentDir3);
				string ResDir = Path.Combine(ParentDir4, "Platform Icons", Part2, FileLogo);
				if (File.Exists(ResDir))
				{
					try
					{
						Image originalImage = System.Drawing.Image.FromFile(ResDir);
						return originalImage;
					}
					catch { }
				}
			}
			return null;
		}

		public void LoadPlatform(IPlatform platform)
		{
			if (platform != null)
			{
				if (!preloadPlatformCategory.ContainsKey(platform.Name))
				{
					if (platform is IPlatformCategory && !preloadPlatformCategory.ContainsKey(platform.Name)) preloadPlatformCategory.Add(platform.Name, platform);
					if(!preloadPlatformIcons.ContainsKey(platform.Name)) preloadPlatformIcons.Add(platform.Name, GetPlatformIcon(platform));
					foreach(var child in platform.GetChildren())
					{
						LoadPlatform(child);
					}
				}

			}
		}

		public void PreloadPlatformName()
		{
			foreach(var p in preloadPlatformCategory)
			{
				if (preloadGames.ContainsKey(p.Key)) continue;
				object objectPlatform = p.Value;
				IGame[] games = null;
				bool found = false;
				if (!found && objectPlatform is IPlaylist)
				{
					var element = (IPlaylist)objectPlatform;
					preloadGames.Add(p.Key, element.GetAllGames(false));
					found = true;
				}
				if (!found && objectPlatform is IPlatformCategory)
				{
					var element = (IPlatformCategory)objectPlatform;
					preloadGames.Add(p.Key, element.GetAllGames(true,true));
					found = true;
				}
				if (!found && objectPlatform is IPlatform)
				{
					var element = (IPlatform)objectPlatform;
					preloadGames.Add(p.Key, element.GetAllGames(true, true));
					found = true;
				}
			}
		}

		private void TextBox1_TextChanged(object sender, EventArgs e)
		{
			texteModifie = true;
		}

		private void Timer1_Tick(object sender, EventArgs e)
		{
			if (texteModifie)
			{
				if (isGeneratingCache) forceCacheReset = true;
				cache.Clear();

				string texte = textBox1.Text; // Obtenez le texte du TextBox

				if (!string.IsNullOrEmpty(texte))
				{
					List<IModelFilter> filter_list = new List<IModelFilter>();
					filter_list.Add(new ModelFilter(delegate (object x)
					{
						return ((IGame)x).Title.Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase);
					}));
					this.fastObjectListView1.ModelFilter = new CompositeAllFilter(filter_list);
				}
				else
				{
					this.fastObjectListView1.ModelFilter = null;
				}


				texteModifie = false; // Réinitialisez l'indicateur de modification
			}
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			List<IModelFilter> filter_list = new List<IModelFilter>();
			filter_list.Add(new ModelFilter(delegate (object x)
			{
				return ((IGame)x).Title.Contains(textBox1.Text, StringComparison.OrdinalIgnoreCase);
			}));
			this.fastObjectListView1.ModelFilter = new CompositeAllFilter(filter_list);
			
		}

		private void fastObjectListView1_ItemActivate(object sender, EventArgs e)
		{
			if (fastObjectListView1.SelectedIndex >= 0)
			{

				IGame game = (IGame)this.fastObjectListView1.SelectedObject;

				var Emulator = PluginHelper.DataManager.GetAllEmulators()
					.Where(emulator => emulator.GetAllEmulatorPlatforms()
						.Where(ep => ep.Platform == game.Platform && ep.IsDefault).Count() > 0).FirstOrDefault();

				if (vlcControl != null && vlcControl.Visible) { vlcControl.Stop(); }
				gameLaunched = true;
				PluginHelper.LaunchBoxMainViewModel.PlayGame(game, null, Emulator, null);

			}
		}

		private void fastObjectListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			platformLoadTimer.Stop();
			imageLoadTimer.Stop();
			
			if (fastObjectListView1.SelectedIndex >= 0)
			{
				selectedGame = (IGame)this.fastObjectListView1.SelectedObject;
				selectedIndex = this.fastObjectListView1.SelectedIndex;
				GameDisplay(false);
				/*
				if (Config.instantShow)
				{
					if (!tempLoad)
					{
						if (vlcControl.Visible)
						{
							vlcControl.Stop();
							vlcControl.Visible = false;
						}
						pictureBox_gameImage.Visible = true;
						int index = fastObjectListView1.SelectedIndex;
						if (cache.ContainsKey(index))
						{
							pictureBox1.Image = cache[index].Logo;
							pictureBox_gameImage.Image = cache[index].Background;
						}
						else
						{
							pictureBox1.Image = LoadingImg;
							pictureBox_gameImage.Image = LoadingImg;
						}

					}

					selectedGame = (IGame)this.fastObjectListView1.SelectedObject;
					selectedIndex = this.fastObjectListView1.SelectedIndex;
					IGame game = selectedGame;
					label_gameTitle.Text = game.Title;
					label_platform.Text = game.Platform;

					lbl_developer.Text = "Developer : " + game.Developer;
					lbl_publisher.Text = "Publisher : " + game.Publisher;
					lbl_file.Text = "File : " + Path.GetFileName(game.ApplicationPath);
					lbl_genre.Text = "Genre : " + game.GenresString;
					lbl_rlzdate.Text = "Release Date : " + game.ReleaseYear != null ? game.ReleaseYear.ToString() : string.Empty;
					lbl_desc.Text = game.Notes;
					if (Config.showExtraInfo)
					{
						treeListView2.Scrollable = false;
						MessageBox.Show(treeListView2.Width.ToString());
						olvColumnValue.Width = 290;
						//olvColumnValue.FillsFreeSpace = true;
						
						if (!treeListView2.Visible) treeListView2.Visible = true;
						treeListView2.SetObjects(GameToNameValueList(game));
					}
					else
					{
						treeListView2.Visible = false;
					}
				}
				else
				{
					selectedGame = (IGame)this.fastObjectListView1.SelectedObject;
					selectedIndex = this.fastObjectListView1.SelectedIndex;
					IGame game = selectedGame;
				}


				imageLoadTimer.Start();
				*/
			}


		}

		private Image ResizeImage(Image img, Size size)
		{
			Bitmap newImage = new Bitmap(size.Width, size.Height);
			using (Graphics g = Graphics.FromImage(newImage))
			{
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.DrawImage(img, 0, 0, size.Width, size.Height);
			}
			img.Dispose();
			return newImage;
		}
		

		private void ImageLoadTimer_Tick(object sender, EventArgs e)
		{
			if (selectedGame != null)
			{
				
				Task.Run(() => GameDisplay(true,pictureBox1.Size,pictureBox_gameImage.Size));
				if (Config.tailleCache > 0 && Config.instantShow)
				{
					if (isGeneratingCache)
					{
						if (selectedIndex >= cacheIndexMin && selectedIndex <= cacheIndexMax)
						{

						}
						else
						{
							if (isGeneratingCache && TaskGenerateCache.Status == TaskStatus.Running)
							{
								forceCacheReset = true;
								TaskGenerateCache.Wait();
								newcache.Clear();
								cacheIndexMin = int.MaxValue;
								cacheIndexMax = int.MaxValue;
							}
						}
					}
					if (!isGeneratingCache)
					{
						TaskGenerateCache = Task.Run(() => GenerateCache());
					}
				}
			}
			imageLoadTimer.Stop();

			/*
			tempLoad = false;

			IGame game = selectedGame;
			int index = selectedIndex;

			label_gameTitle.Text = game.Title;
			label_platform.Text = game.Platform;
			string selectedClearLogoPath = game.ClearLogoImagePath;
			string selectedVideoPath = game.GetVideoPath();
			string selectedBackgroundPath = game.BackgroundImagePath;
			string selectedMainImage = game.Box3DImagePath;

			lbl_developer.Text = "Developer : " + game.Developer;
			lbl_publisher.Text = "Publisher : " + game.Publisher;
			lbl_file.Text = "File : " + Path.GetFileName(game.ApplicationPath);
			lbl_genre.Text = "Genre : " + game.GenresString;
			lbl_rlzdate.Text = "Release Date : " + game.ReleaseYear != null ? game.ReleaseYear.ToString() : string.Empty;
			lbl_desc.Text = game.Notes;

			if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.FrontImagePath;
			if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.CartFrontImagePath;
			if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.Cart3DImagePath;
			ImageDetails[] selectedImgList = game.GetAllImagesWithDetails();
			if (string.IsNullOrEmpty(selectedMainImage) && selectedImgList.Any()) selectedMainImage = selectedImgList.FirstOrDefault().FilePath;

			if (!cache.ContainsKey(index) || !Config.instantShow)
			{
				pictureBox1.Image = GenerateLogo(selectedClearLogoPath, selectedBackgroundPath, label_gameTitle.Text, pictureBox1.Size);
			}
			
			pictureBox1.Visible = true;
			vlcControl.Visible = false;
			pictureBox_gameImage.Visible = false;
			flowLayoutPanelThumbs.Visible = true;

			//flowLayoutPanel1.Visible = true;

			if (!string.IsNullOrEmpty(selectedVideoPath) && Config.showVideo && !gameLaunched)
			{
				try
				{
					//if (Config.volumeVideo == 0 && !vlcControl.Audio.IsMute) vlcControl.Audio.ToggleMute();
					//if (Config.volumeVideo > 0 && vlcControl.Audio.IsMute) vlcControl.Audio.ToggleMute();

					//if (Config.muteVideo) vlcControl.Audio.Volume = 0;
					vlcControl.Visible = true;
					vlcControl.SetMedia(new FileInfo(selectedVideoPath));
					vlcControl.Play();
				}
				catch (Exception ex)
				{
					vlcControl.Stop();
					//MessageBox.Show(ex.Message);
				}
			}
			else vlcControl.Stop();

			if(!vlcControl.Visible && !string.IsNullOrEmpty(selectedMainImage))
			{
				try
				{
					pictureBox_gameImage.Visible = true;
					if (!cache.ContainsKey(index) || !Config.instantShow)
					{
						Image originalImage = System.Drawing.Image.FromFile(selectedMainImage);
						pictureBox_gameImage.Image = ResizeImageBest(originalImage, pictureBox_gameImage.Size);
					}
				}
				catch
				{
					pictureBox_gameImage.Image = null;
				}
			}
			AddPictureBoxesToFlowLayoutPanel(selectedImgList);
			if(Config.tailleCache > 0 && !isGeneratingCache) Task.Run(() => GenerateCache());
			imageLoadTimer.Stop();
			*/


		}

		private void GenerateCache()
		{
			isGeneratingCache = true;
			try {
				int index = fastObjectListView1.GetDisplayOrderOfItemIndex(fastObjectListView1.SelectedItem.Index);
				cacheIndexMin = index - (Config.tailleCache-1);
				cacheIndexMax = index + Config.tailleCache;
				newcache.Clear();
				for (int i = 0; i < Config.tailleCache; i++)
				{
					if (forceCacheReset)
					{
						newcache.Clear();
						forceCacheReset = false;
						isGeneratingCache = false;
						return;
					}
					OLVListItem next = fastObjectListView1.GetNthItemInDisplayOrder(index + (i));
					if (next != null)
					{
						int nextIndex = next.Index;
						if (newcache.ContainsKey(nextIndex))
						{
							continue;
						}
						if (cache.ContainsKey(nextIndex))
						{
							newcache.Add(nextIndex, cache[nextIndex]);
						}
						else
						{
							IGame item = (IGame)next.RowObject;
							string ClearLogoPath = item.ClearLogoImagePath;
							string MainImage = item.Box3DImagePath;
							string BackgroundPath = item.BackgroundImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.FrontImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.CartFrontImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.Cart3DImagePath;
							if (string.IsNullOrEmpty(MainImage))
							{
								ImageDetails[] ImgList = item.GetAllImagesWithDetails();
								if (string.IsNullOrEmpty(MainImage) && ImgList.Any()) MainImage = ImgList.FirstOrDefault().FilePath;
							}
							var NewCacheImg = new CacheImg();
							NewCacheImg.Index = nextIndex;
							NewCacheImg.Logo = GenerateLogo(ClearLogoPath, BackgroundPath, item.Title, pictureBox1.Size);
							NewCacheImg.Background = null;
							if (MainImage != "" && File.Exists(MainImage))
							{
								Image originalImage = System.Drawing.Image.FromFile(MainImage);
								NewCacheImg.Background = ResizeImageBest(originalImage, pictureBox_gameImage.Size);
							}
							newcache.Add(nextIndex, NewCacheImg);
							//cache.Add(nextIndex, NewCacheImg);
						}
					}
					next = fastObjectListView1.GetNthItemInDisplayOrder(index - (i+1));
					if (next != null)
					{
						int nextIndex = next.Index;
						if (newcache.ContainsKey(nextIndex))
						{
							continue;
						}
						if (cache.ContainsKey(nextIndex))
						{
							newcache.Add(nextIndex, cache[nextIndex]);
						}
						else
						{
							IGame item = (IGame)next.RowObject;
							string ClearLogoPath = item.ClearLogoImagePath;
							string MainImage = item.Box3DImagePath;
							string BackgroundPath = item.BackgroundImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.FrontImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.CartFrontImagePath;
							if (string.IsNullOrEmpty(MainImage)) MainImage = item.Cart3DImagePath;
							if (string.IsNullOrEmpty(MainImage))
							{
								ImageDetails[] ImgList = item.GetAllImagesWithDetails();
								if (string.IsNullOrEmpty(MainImage) && ImgList.Any()) MainImage = ImgList.FirstOrDefault().FilePath;
							}
							var NewCacheImg = new CacheImg();
							NewCacheImg.Index = nextIndex;
							NewCacheImg.Logo = GenerateLogo(ClearLogoPath, BackgroundPath, item.Title, pictureBox1.Size);
							NewCacheImg.Background = null;
							if (MainImage != "" && File.Exists(MainImage))
							{
								Image originalImage = System.Drawing.Image.FromFile(MainImage);
								NewCacheImg.Background = ResizeImageBest(originalImage, pictureBox_gameImage.Size);
							}
							newcache.Add(nextIndex, NewCacheImg);
							//cache.Add(nextIndex, NewCacheImg);
						}
					}

				}
				if (forceCacheReset)
				{
					newcache.Clear();
					forceCacheReset = false;
					isGeneratingCache = false;
					return;
				}
				cache = new Dictionary<int, CacheImg>(newcache);
				newcache.Clear();
			}
			catch(Exception ex)
			{

				//MessageBox.Show("msg = " + ex.Message);
			}
			isGeneratingCache = false;
		}

		private void AddPictureBoxesToFlowLayoutPanel(ImageDetails[] selectedImgList, Dictionary<string, Image> preCalculateImg)
		{
			if(flowLayoutPanelThumbs.Visible == false) flowLayoutPanelThumbs.Visible = true;
			// Supprimez tous les contrôles existants dans le FlowLayoutPanel
			flowLayoutPanelThumbs.Controls.Clear();

			// La marge entre chaque PictureBox
			int spacing = 10;

			// Pour chaque image dans votre tableau selectedImgList
			foreach (ImageDetails imgDetails in selectedImgList)
			{
				if (!preCalculateImg.ContainsKey(imgDetails.FilePath)) continue;

				Image ImageThumb = preCalculateImg[imgDetails.FilePath];
				if(ImageThumb == null) continue;
				/*
				if (string.IsNullOrEmpty(imgDetails.FilePath) || !File.Exists(imgDetails.FilePath)) continue;

				Image ImageThumb = null;
				try
				{
					Image originalImage = System.Drawing.Image.FromFile(imgDetails.FilePath);
					ImageThumb = ResizeImageBest(originalImage, new Size(77, 77));
				}
				catch
				{
					continue;
				}
				if (ImageThumb == null) continue;
				*/

				// Créez une nouvelle instance de PictureBox
				PictureBox pictureBox = new PictureBox();

				pictureBox.Anchor = AnchorStyles.Left;

				// Définissez la taille de la PictureBox à 77x77 pixels
				pictureBox.Size = new Size(77, 77);

				// Assurez-vous que la taille de l'image est ajustée à la taille de la PictureBox
				pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

				// Définissez l'image à afficher dans la PictureBox (imgDetails.Image représente votre image)
				pictureBox.Image = ImageThumb;

				// Ajoutez une fonction anonyme pour gérer le clic sur la PictureBox
				pictureBox.Click += (sender, e) =>
				{

					if (vlcControl.Visible)
					{
						vlcControl.Stop();
						vlcControl.Visible = false;
					}

					try
					{
						
						Image originalImage = System.Drawing.Image.FromFile(imgDetails.FilePath);
						pictureBox_gameImage.Image = ResizeImageBest(originalImage, pictureBox_gameImage.Size);
						pictureBox_gameImage.Visible = true;
					}
					catch
					{
						pictureBox_gameImage.Image = null;
					}

				};

				// Ajoutez la PictureBox au FlowLayoutPanel
				flowLayoutPanelThumbs.Controls.Add(pictureBox);

				/*
				// Définissez la marge entre les PictureBox
				flowLayoutPanelThumbs.SetFlowBreak(pictureBox, true);

				// Ajoutez un espacement horizontal entre les PictureBox
				if (flowLayoutPanelThumbs.Controls.Count > 1)
				{
					pictureBox.Margin = new Padding(spacing, 0, 0, 0);
				}
				*/
			}
		}

	
		public Image GenerateLogo(string logoPath, string backgroundPath, string GameName, Size taille)
		{
			if (!string.IsNullOrEmpty(backgroundPath) && backgroundPath.Contains(@"\Platforms\")) backgroundPath = "";

			Size SizeLogo = taille;
			SizeLogo.Width = (int)Math.Round((double)SizeLogo.Width * 0.7);
			SizeLogo.Height = (int)Math.Round((double)SizeLogo.Height * 0.5);

			bool disposeBackground = true;
			Image background = null;
			if (!string.IsNullOrEmpty(backgroundPath) && File.Exists(backgroundPath))
			{
				try
				{
					background = System.Drawing.Image.FromFile(backgroundPath);
					background = ResizeImage(background, taille);
				}
				catch { }

			}
			if(background == null)
			{
				//disposeBackground = false;
				//background = System.Drawing.Image.FromFile(Path.Combine(Config.GetPluginPath(), "background.jpg"));
				//background = ResizeImage(background, taille);
				background = BackgroundEmpty.Clone() as Image;
			}

			Image logo = null;
			if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
			{
				try
				{
					logo = System.Drawing.Image.FromFile(logoPath);
					logo = ResizeImage(logo, SizeLogo);
				}
				catch { }
			}
			if(logo == null)
			{
				logo = GenerateTextImage(GameName, SizeLogo.Width, SizeLogo.Height);
			}

			Bitmap newImage = new Bitmap(taille.Width, taille.Height);
			using (Graphics g = Graphics.FromImage(newImage))
			{
				// Créez une matrice de couleur de transparence (ici, 50% de transparence)
				ColorMatrix colorMatrix = new ColorMatrix();
				colorMatrix.Matrix33 = 0.5f; // Réglez la valeur de transparence entre 0 (complètement transparent) et 1 (complètement opaque)

				// Créez un objet ImageAttributes avec la matrice de couleur
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix);

				// Dessinez l'image de fond avec le filtre de transparence
				g.DrawImage(background, new Rectangle(0, 0, background.Width, background.Height), 0, 0, background.Width, background.Height, GraphicsUnit.Pixel, imageAttributes);


				// Définissez la position où vous souhaitez afficher le logo
				int logoX = (int)Math.Round((taille.Width - SizeLogo.Width) / 2.0); // Coordonnée X
				int logoY = (int)Math.Round((taille.Height - SizeLogo.Height) / 2.0); // Coordonnée Y

				// Dessinez le logo sur l'image de fond
				g.DrawImage(logo, logoX, logoY, logo.Width, logo.Height);
			}
			logo.Dispose();
			if(disposeBackground) background.Dispose();
			return newImage;
		}
		
		/*
		public Image GenerateLogo(string logoPath, string backgroundPath, string GameName, Size taille)
		{
			Image background = null;
			if (!string.IsNullOrEmpty(backgroundPath) && File.Exists(backgroundPath))
			{
				try
				{
					background = System.Drawing.Image.FromFile(backgroundPath);
					if (background.Size != taille)
					{
						background = ResizeImage(background, taille);
					}
				}
				catch { }
			}
			if (background == null)
			{
				background = System.Drawing.Image.FromFile(Path.Combine(Config.GetPluginPath(), "background.jpg"));
				background = ResizeImage(background, taille);
			}

			Size SizeLogo = new Size((int)Math.Round(taille.Width * 0.7), (int)Math.Round(taille.Height * 0.5));

			Image logo = null;
			if (!string.IsNullOrEmpty(logoPath) && File.Exists(logoPath))
			{
				try
				{
					logo = System.Drawing.Image.FromFile(logoPath);
					if (logo.Size != SizeLogo)
					{
						logo = ResizeImage(logo, SizeLogo);
					}
				}
				catch { }
			}
			if (logo == null)
			{
				logo = GenerateTextImage(GameName, SizeLogo.Width, SizeLogo.Height);
			}

			using (Bitmap newImage = new Bitmap(taille.Width, taille.Height))
			using (Graphics g = Graphics.FromImage(newImage))
			{
				// Créez une matrice de couleur de transparence (ici, 50% de transparence)
				ColorMatrix colorMatrix = new ColorMatrix();
				colorMatrix.Matrix33 = 0.5f; // Réglez la valeur de transparence entre 0 (complètement transparent) et 1 (complètement opaque)

				// Créez un objet ImageAttributes avec la matrice de couleur
				ImageAttributes imageAttributes = new ImageAttributes();
				imageAttributes.SetColorMatrix(colorMatrix);

				// Dessinez l'image de fond avec le filtre de transparence
				g.DrawImage(background, new Rectangle(0, 0, background.Width, background.Height), 0, 0, background.Width, background.Height, GraphicsUnit.Pixel, imageAttributes);

				// Définissez la position où vous souhaitez afficher le logo
				int logoX = (int)Math.Round((taille.Width - SizeLogo.Width) / 2.0); // Coordonnée X
				int logoY = (int)Math.Round((taille.Height - SizeLogo.Height) / 2.0); // Coordonnée Y

				// Dessinez le logo sur l'image de fond
				g.DrawImage(logo, logoX, logoY, logo.Width, logo.Height);

				return newImage;
			}
		}
		*/

		/*
		public static Bitmap GenerateTextImage(string text, int width, int height)
		{
			using (Bitmap image = new Bitmap(width, height))
			{
            using (Graphics graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;

                Font baseFont = new Font("Arial", 25); // Police de base de taille 25
                Brush textBrush = new SolidBrush(Color.Red); // Couleur du texte en rouge

                Font fittedFont = GetFittedFont(text, baseFont, graphics, width, height);

                StringFormat format = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                graphics.DrawString(text, fittedFont, textBrush, new RectangleF(0, 0, width, height), format);
            }

				return new Bitmap(image);
			}
		}

		private static Font GetFittedFont(string text, Font initialFont, Graphics graphics, int maxWidth, int maxHeight)
		{
			float fontSize = initialFont.Size;

			while (true)
			{
				Font testFont = new Font(initialFont.FontFamily, fontSize);
				SizeF textSize = graphics.MeasureString(text, testFont);

				if (textSize.Width <= maxWidth && textSize.Height <= maxHeight)
				{
					return testFont;
				}

				fontSize -= 1;

				if (fontSize <= 1)
				{
					return new Font(initialFont.FontFamily, 1); // Font size cannot be less than 1
				}
			}

		}
		*/
		public static Bitmap GenerateTextImage(string text, int width, int maxHeight)
		{
			using (Bitmap image = new Bitmap(width, maxHeight))
			{
				using (Graphics graphics = Graphics.FromImage(image))
				{
					graphics.SmoothingMode = SmoothingMode.AntiAlias;

					Font baseFont = new Font("Arial", 25); // Police de base de taille 25
					Brush textBrush = new SolidBrush(Color.White); // Couleur du texte en rouge

					Font fittedFont = GetFittedFont(text, baseFont, graphics, width, maxHeight);

					StringFormat format = new StringFormat
					{
						Alignment = StringAlignment.Center,
						LineAlignment = StringAlignment.Near
					};

					RectangleF rect = new RectangleF(0, 0, width, maxHeight);

					graphics.DrawString(text, fittedFont, textBrush, rect, format);
				}

				return new Bitmap(image);
			}
		}

		private static Font GetFittedFont(string text, Font initialFont, Graphics graphics, int maxWidth, int maxHeight)
		{
			Font testFont = initialFont;
			float testSize = initialFont.Size;

			while (true)
			{
				testFont = new Font(initialFont.FontFamily, testSize);
				SizeF textSize = graphics.MeasureString(text, testFont, maxWidth);

				if (textSize.Height > maxHeight)
				{
					testSize -= 1;
				}
				else
				{
					break;
				}
			}

			return testFont;
		}

		public static Image ResizeImageBest(Image image, Size newSize)
		{
			if (image == null)
				throw new ArgumentNullException(nameof(image));

			if (newSize.Width <= 0 || newSize.Height <= 0)
				throw new ArgumentException("La taille doit être supérieure à zéro.", nameof(newSize));

			Bitmap newImage = new Bitmap(newSize.Width, newSize.Height);

			using (Graphics graphics = Graphics.FromImage(newImage))
			{
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

				float aspectRatio = (float)image.Width / image.Height;
				int newWidth = image.Width;
				int newHeight = image.Height;
				/*
				int newWidth = newSize.Width;
				int newHeight = newSize.Height;

				if (aspectRatio > 1)
				{
					// L'image est plus large que haute, ajuster en fonction de la largeur
					newHeight = (int)(newWidth / aspectRatio);
				}
				else
				{
					// L'image est plus haute que large, ajuster en fonction de la hauteur
					newWidth = (int)(newHeight * aspectRatio);
				}
				*/
				if (newWidth > newSize.Width || newHeight > newSize.Height)
				{
					if((newWidth/ newSize.Width) > (newHeight / newSize.Height))
					{
						newHeight = (int)Math.Round(((double)newSize.Width / (double)newWidth) * (double)newHeight);
						newWidth = newSize.Width;
					}
					else
					{
						newWidth = (int)Math.Round(((double)newSize.Height / (double)newHeight) * (double)newWidth);
						newHeight = newSize.Height;
					}
				}

				int x = (int)Math.Round((double)(newSize.Width - newWidth) / 2.0);
				int y = (int)Math.Round((double)(newSize.Height - newHeight) / 2.0);

				graphics.DrawImage(image, x, y, newWidth, newHeight);
			}
			image.Dispose();
			return newImage;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			timer_hideLb.Enabled = true;
			//ChangeLaunchboxVisibility(true);
		}

		private void treeListView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			
			if (isGeneratingCache) forceCacheReset = true;
			cache.Clear();
			//IPlatform platform = (IPlatform)this.treeListView1.SelectedObject;
			imageLoadTimer.Stop();
			platformLoadTimer.Stop();

			//ClearDisplay();
			PlateformDisplay((IPlatform)this.treeListView1.SelectedObject,false);
			
			var objectPlatform = this.treeListView1.SelectedObject;
			if (objectPlatform is IPlaylist)
			{
				var element = (IPlaylist)objectPlatform;
				if (preloadGames.ContainsKey(element.Name) && preloadGames[element.Name] != null)
				{
					//MessageBox.Show("debug1");
					this.fastObjectListView1.SetObjects(preloadGames[element.Name]);
					return;
				}

				this.fastObjectListView1.SetObjects(element.GetAllGames(false));
				return;
			}
			if (objectPlatform is IPlatformCategory)
			{
				var element = (IPlatformCategory)objectPlatform;
				if (preloadGames.ContainsKey(element.Name) && preloadGames[element.Name] != null)
				{
					//MessageBox.Show("debug2");
					this.fastObjectListView1.SetObjects(preloadGames[element.Name]);
					return;
				}
				this.fastObjectListView1.SetObjects(element.GetAllGames(true,true));
				return;
			}
			if (objectPlatform is IPlatform)
			{
				var element = (IPlatform)objectPlatform;
				if (preloadGames.ContainsKey(element.Name) && preloadGames[element.Name] != null)
				{
					//MessageBox.Show("debug3");
					this.fastObjectListView1.SetObjects(preloadGames[element.Name]);
					return;
				}
				this.fastObjectListView1.SetObjects(element.GetAllGames(true,true));
				return;
			}
		}

		private void GameDisplay(bool showMedia = false, Size LogoSize = new Size(), Size MainSize = new Size())
		{
			
			IGame game = selectedGame;
			int index = selectedIndex;
			if (showMedia == false)
			{
				if (Config.instantShow)
				{
					if (!tempLoad)
					{
						if (vlcControl.Visible)
						{
							vlcControl.Stop();
							vlcControl.Visible = false;
						}
						pictureBox_gameImage.Visible = true;
						if (cache.ContainsKey(index))
						{
							pictureBox1.Image = cache[index].Logo;
							pictureBox_gameImage.Image = cache[index].Background;
						}
						else if (newcache.ContainsKey(index))
						{
							pictureBox1.Image = newcache[index].Logo;
							pictureBox_gameImage.Image = newcache[index].Background;
						}
						else
						{
							pictureBox1.Image = LoadingImg;
							pictureBox_gameImage.Image = LoadingImg;
						}
					}
					label_gameTitle.Text = game.Title;
					label_platform.Text = game.Platform;

					lbl_developer.Text = "Developer : " + game.Developer;
					lbl_publisher.Text = "Publisher : " + game.Publisher;
					lbl_file.Text = "File : " + Path.GetFileName(game.ApplicationPath);
					lbl_genre.Text = "Genre : " + game.GenresString;
					lbl_rlzdate.Text = "Release Date : " + game.ReleaseYear != null ? game.ReleaseYear.ToString() : string.Empty;
					lbl_desc.Text = game.Notes;
					if (Config.showExtraInfo)
					{
						treeListView2.Scrollable = false;
						olvColumnValue.Width = 290;
						if (!treeListView2.Visible) treeListView2.Visible = true;
						treeListView2.SetObjects(GameToNameValueList(game));
					}
					else
					{
						treeListView2.Visible = false;
					}
				}
				imageLoadTimer.Start();

			}
			else
			{
				tempLoad = true;



				string selectedClearLogoPath = game.ClearLogoImagePath;
				string selectedVideoPath = game.GetVideoPath();
				string selectedBackgroundPath = game.BackgroundImagePath;
				string selectedMainImage = game.Box3DImagePath;

				if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.FrontImagePath;
				if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.CartFrontImagePath;
				if (string.IsNullOrEmpty(selectedMainImage)) selectedMainImage = game.Cart3DImagePath;
				ImageDetails[] selectedImgList = game.GetAllImagesWithDetails();
				if (string.IsNullOrEmpty(selectedMainImage) && selectedImgList.Any()) selectedMainImage = selectedImgList.FirstOrDefault().FilePath;

				Image ImgLogo = null;
				Image ImgMain = null;
				string videoPath = "";

				if (cache.ContainsKey(index))
				{
					ImgLogo = cache[index].Logo;
				}
				else if (newcache.ContainsKey(index))
				{
					ImgLogo = newcache[index].Logo;
				}
				else
				{
					ImgLogo = GenerateLogo(selectedClearLogoPath, selectedBackgroundPath, game.Title, LogoSize);
				}

				if (!string.IsNullOrEmpty(selectedVideoPath) && Config.showVideo && !gameLaunched)
				{
					if (File.Exists(selectedVideoPath))
					{
						videoPath = selectedVideoPath;
					}
				}
				if(string.IsNullOrEmpty(videoPath))
				{
					try
					{
						if (cache.ContainsKey(index))
						{
							ImgMain = cache[index].Background;
						}
						else if (newcache.ContainsKey(index))
						{
							ImgMain = newcache[index].Background;
						}
						else
						{
							Image originalImage = System.Drawing.Image.FromFile(selectedMainImage);
							ImgMain = ResizeImageBest(originalImage, MainSize);
							
						}
					}
					catch { }
				}

				Dictionary<string,Image> preRenderedThumbs = new Dictionary<string,Image>();
				foreach (ImageDetails imgDetails in selectedImgList) 
				{
					if (preRenderedThumbs.ContainsKey(imgDetails.FilePath)) continue;
					if (string.IsNullOrEmpty(imgDetails.FilePath) || !File.Exists(imgDetails.FilePath)) continue;
					Image ImageThumb = null;
					try
					{
						Image originalImage = System.Drawing.Image.FromFile(imgDetails.FilePath);
						ImageThumb = ResizeImageBest(originalImage, new Size(77, 77));
					}
					catch
					{
						ImageThumb = null;
					}
					preRenderedThumbs.Add(imgDetails.FilePath, ImageThumb);
				}

				MethodInvoker methodInvokerDelegate = delegate ()
				{
					label_gameTitle.Text = game.Title;
					label_platform.Text = game.Platform;
					lbl_developer.Text = "Developer : " + game.Developer;
					lbl_publisher.Text = "Publisher : " + game.Publisher;
					lbl_file.Text = "File : " + Path.GetFileName(game.ApplicationPath);
					lbl_genre.Text = "Genre : " + game.GenresString;
					lbl_rlzdate.Text = "Release Date : " + game.ReleaseYear != null ? game.ReleaseYear.ToString() : string.Empty;
					lbl_desc.Text = game.Notes;

					pictureBox1.Image = ImgLogo;
					pictureBox1.Visible = true;
					vlcControl.Visible = false;
					pictureBox_gameImage.Visible = false;
					if (string.IsNullOrEmpty(videoPath))
					{
						pictureBox_gameImage.Image = ImgMain;
						pictureBox_gameImage.Visible = true;
					}
					else
					{
						try
						{
							vlcControl.Visible = true;
							vlcControl.SetMedia(new FileInfo(videoPath));
							vlcControl.Play();
						}
						catch (Exception ex)
						{
							vlcControl.Stop();
						}
					}
					AddPictureBoxesToFlowLayoutPanel(selectedImgList, preRenderedThumbs);

				};

				//This will be true if Current thread is not UI thread.
				if (this.InvokeRequired)
					this.Invoke(methodInvokerDelegate);
				else
					methodInvokerDelegate();

				imageLoadTimer.Stop();
				tempLoad = false;
			}

		}
		private void PlateformDisplay(IPlatform platform, bool showMedia = false)
		{
			if (showMedia == false)
			{
				imageLoadTimer.Stop();
				platformLoadTimer.Stop();
				ClearDisplay();
			}
			
			bool found = false;
			string selectedClearLogoPath = string.Empty;
			string selectedVideoPath = string.Empty;
			string selectedBackgroundPath = string.Empty;
			string selectedMainImage = string.Empty;
			if (!found && platform is IPlaylist)
			{
				var item = (IPlaylist)platform;
				label_gameTitle.Text = item.Name;
				label_platform.Text = "";
				lbl_desc.Text = item.Notes;
				if (showMedia)
				{
					selectedClearLogoPath = item.ClearLogoImagePath;
					selectedVideoPath = item.GetPlatformVideoPath();
					selectedBackgroundPath = item.BackgroundImagePath;
					selectedMainImage = item.DeviceImagePath;
				}
			}
			if (!found && platform is IPlatformCategory)
			{
				var item = (IPlatformCategory)platform;
				label_gameTitle.Text = item.Name;
				label_platform.Text = "";
				lbl_desc.Text = item.Notes;
				if (showMedia)
				{
					selectedClearLogoPath = item.ClearLogoImagePath;
					selectedVideoPath = item.GetPlatformVideoPath();
					selectedBackgroundPath = item.BackgroundImagePath;
					selectedMainImage = item.DeviceImagePath;
				}
			}
			if(!found && platform is IPlatform)
			{
				var item = (IPlatform)platform;
				label_gameTitle.Text = item.Name;
				label_platform.Text = "";
				lbl_desc.Text = item.Notes;
				if (showMedia)
				{
					selectedClearLogoPath = item.ClearLogoImagePath;
					selectedVideoPath = item.GetPlatformVideoPath();
					selectedBackgroundPath = item.BackgroundImagePath;
					selectedMainImage = item.DeviceImagePath;
				}
			}
			pictureBox1.Visible = true;
			vlcControl.Visible = false;
			pictureBox_gameImage.Visible = false;

			if (!showMedia)
			{
				pictureBox1.Image = null;
				selectedPlatform = platform;
				platformLoadTimer.Start();
				return;
			}
			//flowLayoutPanel1.Visible = true;
			pictureBox1.Image = GenerateLogo(selectedClearLogoPath, selectedBackgroundPath, label_gameTitle.Text, pictureBox1.Size);

			if (!string.IsNullOrEmpty(selectedVideoPath) && Config.showVideo && !gameLaunched)
			{
				try
				{
					vlcControl.Visible = true;
					vlcControl.SetMedia(new FileInfo(selectedVideoPath));
					vlcControl.Play();
				}
				catch (Exception ex)
				{
					vlcControl.Stop();
				}
			}
			else vlcControl.Stop();

			if (!vlcControl.Visible && !string.IsNullOrEmpty(selectedMainImage))
			{
				try
				{
					pictureBox_gameImage.Visible = true;
					Image originalImage = System.Drawing.Image.FromFile(selectedMainImage);
					pictureBox_gameImage.Image = ResizeImageBest(originalImage, pictureBox_gameImage.Size);
					
				}
				catch
				{
					pictureBox_gameImage.Image = null;
				}
			}
		}

		private void button1_Click_1(object sender, EventArgs e)
		{
			Config.LaunchboxWasShow = true;
			ChangeLaunchboxVisibility(false);
			this.Close();


		}

		private void ChangeLaunchboxVisibility(bool hide)
		{
			//var process = Process.GetProcessesByName("LaunchBox").FirstOrDefault(p => p.MainWindowTitle != "");
			Process process = Process.GetCurrentProcess();
			int processId = process.Id;
			EnumWindows((hWnd, lParam) =>
			{
				int windowProcessId;
				GetWindowThreadProcessId(hWnd, out windowProcessId);

				if (windowProcessId == (int)lParam)
				{
					StringBuilder windowText = new StringBuilder(256); // Taille maximale du texte de la fenêtre
					GetWindowText(hWnd, windowText, windowText.Capacity);

					//MessageBox.Show($"Handle de fenêtre : {hWnd}, Nom de la fenêtre : {windowText}");
					if (windowText.ToString() == "LaunchBox")
					{
						if (hide)
						{
							ShowWindow(hWnd, SW_HIDE);
							launchboxHidden = true;
						}
						else
						{
							ShowWindow(hWnd, SW_SHOW);
							launchboxHidden = false;
						}
					}
				}
				return true;
			}, (IntPtr)processId);
		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}

		private void timer_hideLb_Tick(object sender, EventArgs e)
		{
			Process process = Process.GetCurrentProcess();
			int processId = process.Id;
			EnumWindows((hWnd, lParam) =>
			{
				int windowProcessId;
				GetWindowThreadProcessId(hWnd, out windowProcessId);

				if (windowProcessId == (int)lParam)
				{
					StringBuilder windowText = new StringBuilder(256); // Taille maximale du texte de la fenêtre
					GetWindowText(hWnd, windowText, windowText.Capacity);

					//MessageBox.Show($"Handle de fenêtre : {hWnd}, Nom de la fenêtre : {windowText}");
					if (windowText.ToString() == "LaunchBox")
					{
						Thread.Sleep(100);
						ShowWindow(hWnd, SW_HIDE);
						timer_hideLb.Enabled = false;
						launchboxHidden = true;
						Thread.Sleep(100);
						this.Activate();
						this.TopMost = true;
						Thread.Sleep(100);
						this.TopMost = false;
					}
				}
				return true;
			}, (IntPtr)processId);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (launchboxHidden)
			{
				if (Config.LaunchboxWasShow)
				{
					//Invoke(new MethodInvoker(PluginHelper.LaunchBoxMainViewModel.Close));
					//PluginHelper.LaunchBoxMainViewModel.Close();
					ChangeLaunchboxVisibility(false);
				}
				else
				{
					Process.GetCurrentProcess().Kill();

				}
			}

		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			var dataToSave = fastObjectListView1.SaveState();
			using (FileStream fs = new FileStream(Config.GetConfigBin(), FileMode.Create))
			{
				fs.Write(dataToSave, 0, dataToSave.Length);
			}
			Config.SaveSize(this.Size);

		}

		private void button2_Click(object sender, EventArgs e)
		{
			var frm = new FormConfig();
			frm.ShowDialog();
		}

		private void tableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
		{

		}

		private void fake_textbox_TextChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void label5_Click(object sender, EventArgs e)
		{

		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}

		private void treeListView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (treeListView1.IsExpanded(treeListView1.SelectedObject))
			{
				treeListView1.Collapse(treeListView1.SelectedObject);
			}
			else
			{
				treeListView1.Expand(treeListView1.SelectedObject);
			}
		}

		private void ClearDisplay()
		{
			lbl_desc.Text = "";
			lbl_developer.Text = "";
			lbl_file.Text = "";
			lbl_genre.Text = "";
			lbl_publisher.Text = "";
			lbl_rlzdate.Text = "";
			label_gameTitle.Text = "";
			label_platform.Text = "";
			pictureBox1.Visible = false;
			pictureBox_gameImage.Visible = false;
			flowLayoutPanelThumbs.Visible = false;
			if (vlcControl != null && vlcControl.Visible)
			{
				vlcControl.Stop();
				vlcControl.Visible = false;
			}
			treeListView2.Visible = false;
		}

		public List<NameValue> GameToNameValueList(IGame game)
		{
			var Result = new List<NameValue>();
			Result.Add(new NameValue("Title", game.Title));
			Result.Add(new NameValue("Developer", game.Developer));
			Result.Add(new NameValue("Publisher", game.Publisher));
			Result.Add(new NameValue("ApplicationPath", game.ApplicationPath));

			int i = 0;
			foreach(var add in game.GetAllAdditionalApplications())
			{
				try
				{
					i++;
					string fileName = Path.GetFileName(add.ApplicationPath);
					Result.Add(new NameValue($"AddApp{i}", fileName));
				}
				catch { }
				
			}

			foreach(var custom in game.GetAllCustomFields())
			{
				Result.Add(new NameValue(custom.Name, custom.Value));
			}



			string ReleaseDate = string.Empty;
			if (game.ReleaseDate != null) {
				DateTime dateTime = (DateTime)game.ReleaseDate;
				ReleaseDate = dateTime.ToShortDateString();
			}
			Result.Add(new NameValue("ReleaseDate", ReleaseDate));
			Result.Add(new NameValue("ReleaseYear", game.ReleaseYear == null ? string.Empty : game.ReleaseYear.ToString()));
			Result.Add(new NameValue("Rating", game.Rating));
			//Result.Add(new NameValue("Genres", game.GenresString));

			i = 0;
			foreach (var genre in game.Genres)
			{
				i++;
				Result.Add(new NameValue($"Genre {i}", genre));
			}

			Result.Add(new NameValue("Series", game.Series));
			Result.Add(new NameValue("Region", game.Region));
			Result.Add(new NameValue("PlayMode", game.PlayMode));
			Result.Add(new NameValue("Version", game.Version));
			Result.Add(new NameValue("Status", game.Status));
			Result.Add(new NameValue("Source", game.Source));
			Result.Add(new NameValue("DateAdded", game.DateAdded.ToShortTimeString()));
			Result.Add(new NameValue("DateModified", game.DateModified.ToShortTimeString()));
			Result.Add(new NameValue("PlayCount", game.PlayCount.ToString()));
			Result.Add(new NameValue("Favorite", game.Favorite ? "Yes" : "No"));
			Result.Add(new NameValue("Completed", game.Completed ? "Yes" : "No"));
			Result.Add(new NameValue("Broken", game.Broken ? "Yes" : "No"));
			Result.Add(new NameValue("Portable", game.Portable ? "Yes" : "No"));
			Result.Add(new NameValue("Hide", game.Hide ? "Yes" : "No"));
			Result.Add(new NameValue("StarRating", game.StarRating.ToString()));
			Result.Add(new NameValue("CommunityStarRating", Math.Round((float)game.CommunityStarRating, 2).ToString()));
			Result.Add(new NameValue("CommunityStarRatingTotalVotes", game.CommunityStarRatingTotalVotes.ToString()));

			i = 0;
			foreach (IAlternateName name in game.GetAllAlternateNames())
			{
				i++;
				Result.Add(new NameValue($"AltName{i}", name.Name));
			}

			Result.Add(new NameValue("WikipediaUrl", game.WikipediaUrl.ToString()));
			Result.Add(new NameValue("VideoUrl", game.VideoUrl));
			
			string gameInstalled = string.Empty;
			if (game.Installed != null) gameInstalled = (bool)game.Installed ? "Yes" : "No";
			Result.Add(new NameValue("Installed", gameInstalled));

			string launchBoxDbId = string.Empty;
			if (game.LaunchBoxDbId != null) launchBoxDbId = game.LaunchBoxDbId.ToString();
			Result.Add(new NameValue("LaunchBoxDbId", launchBoxDbId));

			TimeSpan temps = TimeSpan.FromSeconds(game.PlayTime);
			Result.Add(new NameValue("PlayTime", temps.ToString(@"hh\:mm\:ss")));

			return Result;
		}

		private void treeListView2_SelectedIndexChanged(object sender, EventArgs e)
		{
			treeListView2.Scrollable = true;
			//olvColumnValue.FillsFreeSpace = false;
			olvColumnValue.Width = 1000;
		}

		private void treeListView2_IsHyperlink(object sender, IsHyperlinkEventArgs e)
		{
			if (!e.Url.StartsWith("http")) e.Url = null;
		}

		private void treeListView2_Leave(object sender, EventArgs e)
		{
			treeListView2.Refresh();
			olvColumnValue.Width = 290;
			treeListView2.RebuildColumns();
		}
	}
	public static class StringExtensions
	{
		public static bool Contains(this string source, string toCheck, StringComparison comp)
		{
			return source?.IndexOf(toCheck, comp) >= 0;
		}
	}



	public struct NameValue
	{
		public string Name;
		public string Value;

		public NameValue(string name, string value)
		{
			Name = name;
			Value = value;
		}
	}


	public class CacheImg : IDisposable
	{
		public int Index;
		public Image Logo = null;
		public Image Background = null;

		public void Dispose()
		{
			Logo.Dispose();
			Background.Dispose();
		}
	}
	/*
	public class GameDetail
	{
		public int Index;
		public string Title;
		public string Platform;
		public string Description;
		public string ClearLogoImagePath;
		public string VideoPath;
		public string BackgroundPath;
		public string MainImage;

		public string Developer;
		public string Publisher;
		public string File;
		public string Genre;
		public string RlzDate;
		public string Desc;
		ImageDetails[] ImgList;

		Image ImageLogo = null;
		Image ImageMain = null;

		public GameDetail(int index, IGame game, Size SizeLogo, Size SizeMain)
		{
			Index = index;
			Title = game.Title;
			Platform = game.Platform;
			ClearLogoImagePath = game.ClearLogoImagePath;
			VideoPath = game.GetVideoPath();
			BackgroundPath = game.BackgroundImagePath;
			MainImage = game.Box3DImagePath;
			if (string.IsNullOrEmpty(MainImage)) MainImage = game.FrontImagePath;
			if (string.IsNullOrEmpty(MainImage)) MainImage = game.CartFrontImagePath;
			if (string.IsNullOrEmpty(MainImage)) MainImage = game.Cart3DImagePath;

			Developer = game.Developer;
			Publisher = game.Publisher;
			Genre = game.GenresString;
			RlzDate = game.ReleaseYear != null ? game.ReleaseYear.ToString() : string.Empty;
			Desc = game.Notes;

			ImgList = game.GetAllImagesWithDetails();
			if (string.IsNullOrEmpty(MainImage) && ImgList.Any()) MainImage = ImgList.FirstOrDefault().FilePath;
			ImageLogo = Form1.GenerateLogo(ClearLogoImagePath, BackgroundPath, Title, SizeLogo);
			//ImageMain = Form1.GenerateLogo(ClearLogoImagePath, BackgroundPath, Title, SizeLogo);

		}
	}
	*/


}
