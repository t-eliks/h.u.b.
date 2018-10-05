namespace HUB.Views.Blend
{
    using System;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using HUB.Logic;
    using HUB.ViewModels;
    using System.Threading;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Interaction logic for MainView_Blend.xaml
    /// </summary>
    public partial class MainView_Blend : Window
    {
        public static readonly DependencyProperty ListBoxItemWidthProperty = DependencyProperty.Register("ListBoxItemWidth",
            typeof(int), typeof(MainView_Blend));

        public int ListBoxItemWidth
        {
            get { return (int)GetValue(ListBoxItemWidthProperty); }
            set { SetValue(ListBoxItemWidthProperty, value); }
        }

        public static readonly DependencyProperty ListBoxHeightProperty = DependencyProperty.Register("ListBoxItemHeight",
            typeof(int), typeof(MainView_Blend));

        public int ListBoxItemHeight
        {
            get { return (int)GetValue(ListBoxHeightProperty); }
            set { SetValue(ListBoxHeightProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemImageHeightProperty = DependencyProperty.Register("ListBoxItemImageHeight",
            typeof(int), typeof(MainView_Blend));

        public int ListBoxItemImageHeight
        {
            get { return (int)GetValue(ListBoxItemImageHeightProperty); }
            set { SetValue(ListBoxItemImageHeightProperty, value); }
        }

        public static readonly DependencyProperty WindowWidthProperty = DependencyProperty.Register("WindowWidth",
            typeof(double), typeof(MainView_Blend), new FrameworkPropertyMetadata(1400.0));

        public double WindowWidth
        {
            get { return (double)GetValue(WindowWidthProperty); }
            set { SetValue(WindowWidthProperty, value); }
        }

        public static readonly DependencyProperty WindowHeightProperty = DependencyProperty.Register("WindowHeight",
            typeof(double), typeof(MainView_Blend), new FrameworkPropertyMetadata(800.0));

        public double WindowHeight
        {
            get { return (double)GetValue(WindowHeightProperty); }
            set { SetValue(WindowHeightProperty, value); }
        }

        private System.Windows.Forms.NotifyIcon _notifyicon;

        public bool MinimizeToTray
        {
            get
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                return bool.Parse(config.AppSettings.Settings["minimizetotray"].Value);
            }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["minimizetotray"].Value = value.ToString();

                config.Save();
            }
        }

        public bool CloseOnLaunch
        {
            get
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                return bool.Parse(config.AppSettings.Settings["closeonlaunch"].Value);
            }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["closeonlaunch"].Value = value.ToString();

                config.Save();
            }
        }

        public bool WebsiteCloseOnLaunch
        {
            get
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                return bool.Parse(config.AppSettings.Settings["websitecloseonlaunch"].Value);
            }
            set
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["websitecloseonlaunch"].Value = value.ToString();

                config.Save();
            }
        }

        private const string Guid = "250C5597-BA73-40DF-B2CF-DD644F044834";
        static readonly Mutex Mutex = new Mutex(true, "{" + Guid + "}");

        public MainView_Blend()
        {
            if (!Mutex.WaitOne(TimeSpan.Zero, true))
            {
                MessageBox.Show("Instance of application already running.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();
                MainViewModel datacontext = new MainViewModel();
                DataContext = datacontext;
                datacontext.OnGameDialogSetUp += Open_AddEditGameDialogView;
                datacontext.OnGameEdit += Open_AddEditGameDialogView;
                datacontext.OnWebsiteDialogSetUp += Open_AddEditWebsiteDialogView;
                datacontext.OnWebsiteEdit += Open_AddEditWebsiteDialogView;
                datacontext.OnEventDialogSetUp += Open_AddEditEventDialog;
                datacontext.OnEventEdit += Open_AddEditEventDialog;
                datacontext.OnWebsiteDetailsSetUp += Open_WebsiteDetails;

                var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                ListBoxItemWidth = int.Parse(cfg.AppSettings.Settings["listboxitemwidth"].Value);
                ListBoxItemHeight = int.Parse(cfg.AppSettings.Settings["listboxitemheight"].Value);
                ListBoxItemImageHeight = int.Parse(cfg.AppSettings.Settings["listboxitemimageheight"].Value);

                WindowWidth = int.Parse(cfg.AppSettings.Settings["windowwidth"].Value);
                WindowHeight = int.Parse(cfg.AppSettings.Settings["windowheight"].Value);

                InitializeSettings();
                InitializeNotifyIcon();

                //if (File.Exists("homebuttonscript.ahk"))
                //    Process.Start("homebuttonscript.ahk");
            }      
        }

        private void InitializeSettings()
        {
            gameGrid.Background = new ImageBrush() { ImageSource = Initialize_Bitmap(new Uri(ConfigurationManager.AppSettings["gamebackground"]), 1024, 1024), Opacity = 0.8 };
            websiteGrid.Background = new ImageBrush() { ImageSource = Initialize_Bitmap(new Uri(ConfigurationManager.AppSettings["websitebackground"]), 1024, 1024), Opacity = 0.8 };
            menuPanel.Background = new ImageBrush() { ImageSource = Initialize_Bitmap(new Uri(ConfigurationManager.AppSettings["eventbackground"]), 512, 1024), Stretch = Stretch.UniformToFill };

            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            regsitefiltercheckbox.IsChecked = bool.Parse(cfg.AppSettings.Settings["regsitesfiltered"].Value);
            showfiltercheckbox.IsChecked = bool.Parse(cfg.AppSettings.Settings["showsfiltered"].Value);
            gamesfilteredcheckbox.IsChecked = bool.Parse(cfg.AppSettings.Settings["uninstalledgamesfiltered"].Value);

            ((MainViewModel)this.DataContext).WebsiteLaunched += delegate
            {
                if (WebsiteCloseOnLaunch)
                {
                    if (this.WindowState == WindowState.Normal)
                        this.WindowState = WindowState.Minimized;
                }
            };

            ((MainViewModel)this.DataContext).ApplicationLaunched += delegate
            {
                if (!CloseOnLaunch)
                {
                    if (this.WindowState == WindowState.Normal)
                        this.WindowState = WindowState.Minimized;
                }
                else
                    Application.Current.Shutdown();
            };

            this.StateChanged += delegate
            {
                if (MinimizeToTray)
                {
                    if (this.WindowState == WindowState.Minimized)
                    {
                        this.ShowInTaskbar = false;

                        _notifyicon.Visible = true;
                    }
                    else if (this.WindowState == WindowState.Normal)
                    {
                        _notifyicon.Visible = false;
                        this.ShowInTaskbar = true;
                        this.Activate();
                    }
                }
            };
        }

        private void InitializeNotifyIcon()
        {
            _notifyicon = new System.Windows.Forms.NotifyIcon();
            _notifyicon.Icon = new System.Drawing.Icon(@"C:\Users\Rokas\Documents\Personal\C#\HUB\HUB\Icons\tray.ico");

            System.Windows.Forms.MenuItem menuitem1 = new System.Windows.Forms.MenuItem()
            {
                Index = 0,
                Text = "Open"
            };

            menuitem1.Click += delegate {
                if (this.WindowState != WindowState.Normal)
                    this.WindowState = WindowState.Normal;
            };

            System.Windows.Forms.MenuItem menuitem2 = new System.Windows.Forms.MenuItem()
            {
                Index = 1,
                Text = "Exit"
            };

            menuitem2.Click += delegate {
                Application.Current.Shutdown();
            };

            _notifyicon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { menuitem1, new System.Windows.Forms.MenuItem() { Text = "-" }, menuitem2 });

            _notifyicon.DoubleClick += delegate
            {
                this.WindowState = WindowState.Normal;
            };
        }

        private void ChooseBackground_Button(object sender, RoutedEventArgs e)
        {
            Choose_Background(((Button)sender).Uid);
        }

        private void Choose_Background(string setting)
        {
            var image = FileSelectHandler.ChooseImageLocation();

            if (image != null)
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings[setting].Value = image;

                var bmp = Initialize_Bitmap(new Uri(config.AppSettings.Settings[setting].Value), 1024, 1024);

                switch (setting)
                {
                    case "gamebackground":
                        gameGrid.Background.SetValue(ImageBrush.ImageSourceProperty, bmp);
                        break;
                    case "websitebackground":
                        websiteGrid.Background.SetValue(ImageBrush.ImageSourceProperty, bmp);
                        break;
                    case "eventbackground":
                        menuPanel.Background.SetValue(ImageBrush.ImageSourceProperty, bmp);
                        break;
                    default:
                        break;
                }

                config.Save();
            }
        }

        private BitmapImage Initialize_Bitmap(Uri uri, int decodepixelwidth, int decodepixelheight)
        {
            try
            {
                var gamebg = new BitmapImage();

                gamebg.BeginInit();
                gamebg.CacheOption = BitmapCacheOption.OnLoad;
                gamebg.UriSource = uri;
                gamebg.DecodePixelWidth = decodepixelwidth;
                gamebg.DecodePixelHeight = decodepixelheight;
                gamebg.EndInit();

                return gamebg;
            }
            catch
            {

            }
            return null;
        }

        private void Clear_Thumbnails(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            Collection<string> uifiles = new Collection<string>
            {
                Path.GetFileName(config.AppSettings.Settings["gamebackground"].Value),
                Path.GetFileName(config.AppSettings.Settings["websitebackground"].Value),
                Path.GetFileName(config.AppSettings.Settings["eventbackground"].Value)
            };

            ((Button)sender).Command = ((MainViewModel)this.DataContext).ClearUnusedThumbnailsCommand;

            ((Button)sender).CommandParameter = uifiles;

            ((Button)sender).Command.Execute(uifiles);
        }

        private void Open_AddEditEventDialog(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                addEditEventDialog.Visibility = Visibility.Visible;
                addEditEventDialog.DataContext = ((MainViewModel)this.DataContext).AddEventDialogViewModel;

                (menuPanel.Resources["addEventExpand"] as Storyboard).Begin();

                ((MainViewModel)this.DataContext).AddEventDialogViewModel.OnAddedEvent += delegate
                {
                    Storyboard a = (menuPanel.Resources["addEventRetract"] as Storyboard);

                    a.Completed += delegate
                    {
                        addEditEventDialog.Visibility = Visibility.Collapsed;
                    };
                    a.Begin();
                };
            }
        }

        private void Open_AddEditGameDialogView(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                addGameDialogView.Visibility = Visibility.Visible;
                addGameDialogView.DataContext = ((MainViewModel)this.DataContext).AddGameDialogViewModel;

                (addGameDialogView.Resources["addGameExpand"] as Storyboard).Begin();

                ((MainViewModel)this.DataContext).AddGameDialogViewModel.OnItemAdded += delegate
                {
                    Storyboard a = (addGameDialogView.Resources["addGameRetract"] as Storyboard);
                    a.Completed += delegate
                    {
                        addGameDialogView.Visibility = Visibility.Collapsed;
                    };
                    a.Begin();
                };
            }
        }

        private void Open_AddEditWebsiteDialogView(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                addWebsiteDialogView.Visibility = Visibility.Visible;
                addWebsiteDialogView.DataContext = ((MainViewModel)this.DataContext).AddWebsiteDialogViewModel;

                (addWebsiteDialogView.Resources["addWebsiteExpand"] as Storyboard).Begin();

                ((MainViewModel)this.DataContext).AddWebsiteDialogViewModel.OnItemAdded += delegate
                {
                    Storyboard a = (addWebsiteDialogView.Resources["addWebsiteRetract"] as Storyboard);
                    a.Completed += delegate
                    {
                        addWebsiteDialogView.Visibility = Visibility.Collapsed;
                    };
                    a.Begin();
                };
            }
        }

        private void Open_WebsiteDetails(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                websiteDetailsDialogView.Visibility = Visibility.Visible;
                websiteDetailsDialogView.DataContext = ((MainViewModel)this.DataContext).WebsiteDetailsViewModel;

                (websiteDockPanel.Resources["websiteDetailsExpand"] as Storyboard).Begin();

                ((MainViewModel)this.DataContext).WebsiteDetailsViewModel.OnClose += delegate
                {
                    Storyboard a = (websiteDockPanel.Resources["websiteDetailsRetract"] as Storyboard);
                    a.Completed += delegate
                    {
                        websiteDetailsDialogView.Visibility = Visibility.Collapsed;
                    };
                    a.Begin();
                };
            }
        }

        private void Save_Settings(object sender, RoutedEventArgs e)
        {
            var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            cfg.AppSettings.Settings["listboxitemwidth"].Value = ListBoxItemWidth.ToString();
            cfg.AppSettings.Settings["listboxitemheight"].Value = ListBoxItemHeight.ToString();
            cfg.AppSettings.Settings["listboxitemimageheight"].Value = ListBoxItemImageHeight.ToString();
            cfg.AppSettings.Settings["windowwidth"].Value = WindowWidth.ToString();
            cfg.AppSettings.Settings["windowheight"].Value = WindowHeight.ToString();

            cfg.Save();
        }
    }
}
