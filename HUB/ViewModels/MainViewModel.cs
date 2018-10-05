namespace HUB.ViewModels
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Diagnostics;
    using System.Windows.Input;
    using System.Collections.ObjectModel;

    using HUB.Models;
    using HUB.Logic;
    using HUB.ViewModels.DialogViewModels;
    using HUB.Logic.Dialog;
    using System.Collections.Generic;
    using System.Linq;
    using System.ComponentModel;
    using System.Windows.Data;
    using System.Configuration;

    public partial class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            _dialogService = new DialogService();

            DeserializeObjects();

            if (Games == null)
                Games = new ObservableCollection<VideoGame>();

            if (Websites == null)
                Websites = new ObservableCollection<Website>();

            if (Events == null)
                Events = new ObservableCollection<Event>();

            SerializeObjectsCommand = new RelayCommand(SerializeObjects);
            EditObjectCommand = new RelayCommand<Item>(EditObject);
            RemoveObjectCommand = new RelayCommand<Item>(RemoveObject);
            OpenObjectCommand = new RelayCommand<Item>(OpenObject);
            ApplyFilterCommand = new RelayCommand<string>(ApplyFilter);
            ClearUnusedThumbnailsCommand = new RelayCommand<Collection<string>>(ClearUnusedThumbnails);
            OpenGameDirectoryCommand = new RelayCommand<Item>(OpenGameDirectory);
            ToggleGameInstalledCommand = new RelayCommand<VideoGame>(ToggleGameInstalled);

            SetUpGameDialogCommand = new RelayCommand(SetUpGameDialog);
            SetUpWebsiteDialogCommand = new RelayCommand(SetUpWebsiteDialog);
            SetUpEventDialogCommand = new RelayCommand(SetUpEventDialog);

            EditEventCommand = new RelayCommand<Event>(EditEvent);
            RemoveEventCommand = new RelayCommand<Event>(RemoveEvent);
            RefreshEventsCommand = new RelayCommand(delegate
            {
                RaisePropertyChanged(nameof(EventsSorted));
            });

            ApplicationLaunched = new RoutedEventHandler((sender, e) => { });
            WebsiteLaunched = new RoutedEventHandler((sender, e) => { });
        }

        #region ICommands

        public ICommand SerializeObjectsCommand { get; private set; }

        public ICommand EditObjectCommand { get; private set; }

        public ICommand RemoveObjectCommand { get; private set; }

        public ICommand OpenObjectCommand { get; private set; }

        public ICommand SetUpGameDialogCommand { get; private set; }

        public ICommand SetUpWebsiteDialogCommand { get; private set; }

        public ICommand ApplyFilterCommand { get; private set; }

        public ICommand ClearUnusedThumbnailsCommand { get; private set; }

        public ICommand OpenGameDirectoryCommand { get; private set; }

        public ICommand SetUpEventDialogCommand { get; private set; }

        public ICommand EditEventCommand { get; private set; }

        public ICommand RemoveEventCommand { get; private set; }

        public ICommand RefreshEventsCommand { get; private set; }

        public ICommand ToggleGameInstalledCommand { get; private set; }

        #endregion

        public AddEditEventViewModel AddEventDialogViewModel;

        public AddGameDialogViewModel AddGameDialogViewModel;

        public AddWebsiteDialogViewModel AddWebsiteDialogViewModel;

        public WebsiteDetailsViewModel WebsiteDetailsViewModel;

        #region Fields and Properties

        internal static DialogService _dialogService;

        public event RoutedEventHandler ApplicationLaunched;

        public event RoutedEventHandler WebsiteLaunched;

        public event RoutedEventHandler OnGameEdit;

        public event RoutedEventHandler OnWebsiteEdit;

        public event RoutedEventHandler OnGameDialogSetUp;

        public event RoutedEventHandler OnWebsiteDialogSetUp;

        public event RoutedEventHandler OnEventDialogSetUp;

        public event RoutedEventHandler OnEventEdit;

        public event RoutedEventHandler OnWebsiteDetailsSetUp;

        private ObservableCollection<VideoGame> _Games;

        public ObservableCollection<VideoGame> Games
        {
            get
            {
                return _Games;
            }
            set
            {
                if (_Games != value)
                {
                    _Games = value;
                    RaisePropertyChanged(nameof(Games));
                }
            }
        }

        private ObservableCollection<Website> _Websites;

        public ObservableCollection<Website> Websites
        {
            get
            {
                return _Websites;
            }
            set
            {
                if (_Websites != value)
                {
                    _Websites = value;
                    RaisePropertyChanged(nameof(Websites));
                }
            }
        }
      
        private ObservableCollection<Event> _Events;

        public ObservableCollection<Event> Events
        {
            get
            {
                return _Events;
            }

            set
            {
                if (_Events != value)

                {
                    _Events = value;

                    RaisePropertyChanged(nameof(Events));
                }
            }
        }

        public ICollectionView EventsSorted
        {
            get
            {
                var source = CollectionViewSource.GetDefaultView(Events);
                source.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Ascending));
                return source;
            }
        }

        public string EventHeader { get { return Events.Count == 0 ? "No upcoming events" : "Upcoming events: " + Events.Count; } }

        public bool PopulatedGameList
        {
            get
            {
                return Games.Count > 0;
            }
        }

        public bool PopulatedWebsiteList
        {
            get
            {
                return Websites.Count > 0;
            }
        }

        private bool _UnsavedChanges;

        public bool UnsavedChanges
        {
            get
            {
                return _UnsavedChanges;
            }
            set
            {
                if (_UnsavedChanges != value)
                {
                    _UnsavedChanges = value;

                    RaisePropertyChanged(nameof(UnsavedChanges));
                }
            }
        }

        #endregion

        #region Methods and Functions

        private void SerializeObjects()
        {
            ObjectSerializationHandler.SerializeObject(new DataContainer() { Games = this.Games, Websites = this.Websites, Events = this.Events});

            if (UnsavedChanges)
                UnsavedChanges = false;
        }

        private void DeserializeObjects()
        {
            DataContainer container = ((DataContainer)((ObjectSerializationHandler.DeserializeObject(Directory.GetCurrentDirectory() + "/data.bin"))));

            Games = container?.Games;

            Websites = container?.Websites;

            Events = container?.Events;
        }

        private void EditObject(Item item)
        {
            if (item is VideoGame game)
                EditGame(game);

            else if (item is Website website)
                EditWebsite(website);

            if (!UnsavedChanges)
                UnsavedChanges = true;
        }

        private void EditGame(VideoGame game)
        {
            AddGameDialogViewModel = new AddGameDialogViewModel();
            AddGameDialogViewModel.Item = game;
            AddGameDialogViewModel.EditMode = true;
            AddGameDialogViewModel.Title = game.Title;
            AddGameDialogViewModel.GameLocation = game.GameLocation;
            AddGameDialogViewModel.ImageLocation = game.ImageLocation;
            AddGameDialogViewModel.TargetParameters = game.TargetParameters;
            AddGameDialogViewModel.EdgeColor = game.EdgeColor;
            AddGameDialogViewModel.MiddleColor = game.MiddleColor;
            AddGameDialogViewModel.HighlightColor = game.HighlightColor;
            AddGameDialogViewModel.ContextMenuMap = game.ContextMenuMap;

            OnGameEdit?.Invoke(this, new RoutedEventArgs());
        }

        private void SetUpGameDialog()
        {
            AddGameDialogViewModel = new AddGameDialogViewModel();

            AddGameDialogViewModel.OnItemAdded += (s, e) =>
            {
                if (((DialogRoutedEventArgs)e).Object != null)
                    AddGame((VideoGame)(((DialogRoutedEventArgs)e).Object));
            };

            OnGameDialogSetUp?.Invoke(this, new RoutedEventArgs());
        }

        private void EditEvent(Event evnt)
        {
            AddEventDialogViewModel = new AddEditEventViewModel();
            AddEventDialogViewModel.Event = evnt;
            AddEventDialogViewModel.EditMode = true;
            AddEventDialogViewModel.Text = evnt.Text;
            AddEventDialogViewModel.Subtext = evnt.Subtext;
            AddEventDialogViewModel.Date = evnt.Date;
            AddEventDialogViewModel.Hour = evnt.Date.Hour;
            AddEventDialogViewModel.Minutes = evnt.Date.Minute;

            if (!UnsavedChanges)
                UnsavedChanges = true;

            RaisePropertyChanged(nameof(EventsSorted));

            OnEventEdit?.Invoke(this, new RoutedEventArgs());
        }

        private void EditWebsite(Website website)
        {
            AddWebsiteDialogViewModel = new AddWebsiteDialogViewModel();
            AddWebsiteDialogViewModel.Item = website;
            AddWebsiteDialogViewModel.EditMode = true;
            AddWebsiteDialogViewModel.Title = website.Title;
            AddWebsiteDialogViewModel.Link = website.Link;
            AddWebsiteDialogViewModel.Description = website.Description;
            AddWebsiteDialogViewModel.ImageLocation = website.ImageLocation;
            AddWebsiteDialogViewModel.EdgeColor = website.EdgeColor;
            AddWebsiteDialogViewModel.MiddleColor = website.MiddleColor;
            AddWebsiteDialogViewModel.HighlightColor = website.HighlightColor;
            AddWebsiteDialogViewModel.ContextMenuMap = website.ContextMenuMap;

            OnWebsiteEdit?.Invoke(this, new RoutedEventArgs());
        }

        private void SetUpWebsiteDialog()
        {
            AddWebsiteDialogViewModel = new AddWebsiteDialogViewModel();

            AddWebsiteDialogViewModel.OnItemAdded += (s, e) =>
            {
                if (((DialogRoutedEventArgs)e).Object != null)
                    AddWebsite((Website)(((DialogRoutedEventArgs)e).Object));
            };

            OnWebsiteDialogSetUp?.Invoke(this, new RoutedEventArgs());
        }

        private void SetUpEventDialog()
        {
            AddEventDialogViewModel = new AddEditEventViewModel();

            AddEventDialogViewModel.OnAddedEvent += (s, e) =>
            {
                if (((DialogRoutedEventArgs)e).Object != null)
                {
                    Events.Add((Event)((DialogRoutedEventArgs)e).Object);

                    if (!UnsavedChanges)
                        UnsavedChanges = true;

                    RaisePropertyChanged(nameof(EventHeader));
                    RaisePropertyChanged(nameof(EventsSorted));
                }
            };

            OnEventDialogSetUp?.Invoke(this, new RoutedEventArgs());
        }

        private void RemoveObject(Item item)
        {
            if (item is VideoGame game)
                Games.Remove(game);

            else if (item is Website website)
                Websites.Remove(website);

            if (!UnsavedChanges)
                UnsavedChanges = true;
        }

        private void OpenObject(Item item)
        {
            if (item is VideoGame game)
                OpenGame(game);

            else if (item is Website website)
                OpenWebsite(website);
        }

        private void ToggleGameInstalled(VideoGame game)
        {
            if (game != null)
            {
                game.Installed = !game.Installed;

                if (!UnsavedChanges)
                    UnsavedChanges = true;
            }
        }

        private void OpenGame(VideoGame game)
        {
            try
            {
                Process.Start(game.GameLocation, game.TargetParameters);
                ApplicationLaunched(this, new RoutedEventArgs());
            }
            catch
            {
                MessageBox.Show("Something went wrong. Make sure launcher path and target parameters are correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenWebsite(Website website)
        {
            if (!(website is TVShow))
            {
                try
                {
                    Process.Start(website.Link);
                    WebsiteLaunched(this, new RoutedEventArgs());
                }
                catch
                {
                    MessageBox.Show("Something went wrong. Make sure path or link is correct.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                WebsiteDetailsViewModel = new WebsiteDetailsViewModel();
                WebsiteDetailsViewModel.Website = (TVShow)website;

                OnWebsiteDetailsSetUp?.Invoke(this, new RoutedEventArgs());

                if (!UnsavedChanges)
                    UnsavedChanges = true;
            }
        }

        private void AddWebsite(Website website)
        {
            if (website != null)
            {
                Websites.Add(website);

                if (!UnsavedChanges)
                    UnsavedChanges = true;

                RaisePropertyChanged(nameof(PopulatedWebsiteList));
            }
        }

        private void AddGame(VideoGame game)
        {
            if (game != null)
            {
                Games.Add(game);

                if (!UnsavedChanges)
                    UnsavedChanges = true;

                RaisePropertyChanged(nameof(PopulatedGameList));
            }
        }

        private void ApplyFilter(string checkboxContent)
        {
            if (String.Compare(checkboxContent, "Regular") == 0)
            {
                bool changed = false;

                foreach (Website website in Websites)
                {
                    if (website is Website && !(website is TVShow))
                    {
                        website.Filtered = !website.Filtered;

                        if (!changed)
                            changed = true;
                    }
                }

                if (changed)
                {
                    var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    cfg.AppSettings.Settings["regsitesfiltered"].Value = (!(bool.Parse(cfg.AppSettings.Settings["regsitesfiltered"].Value))).ToString();

                    cfg.Save();
                }
            }
            else if (String.Compare(checkboxContent, "TV Show") == 0)
            {
                bool changed = false;

                foreach (Website website in Websites)
                {
                    if (website is TVShow)
                    {
                        website.Filtered = !website.Filtered;
                        
                        if (!changed)
                            changed = true;
                    }
                }

                if (changed)
                {
                    var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    cfg.AppSettings.Settings["showsfiltered"].Value = (!(bool.Parse(cfg.AppSettings.Settings["showsfiltered"].Value))).ToString();

                    cfg.Save();
                }
            }
            else if (String.Compare(checkboxContent, "Uninstalled games") == 0)
            {
                bool changed = false;

                foreach (VideoGame game in Games)
                {
                    if (!game.Installed)
                        game.Filtered = !game.Filtered;

                    if (!changed)
                        changed = true;
                }

                if (changed)
                {
                    var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    cfg.AppSettings.Settings["uninstalledgamesfiltered"].Value = (!(bool.Parse(cfg.AppSettings.Settings["uninstalledgamesfiltered"].Value))).ToString();

                    cfg.Save();
                }
            }

            if (!UnsavedChanges)
                UnsavedChanges = true;
        }

        private void ClearUnusedThumbnails(Collection<string> uifiles)
        {
            Collection<string> thumbnails = new Collection<string>();

            foreach (Item item in Games)
            {
                thumbnails.Add(Path.GetFileName(item.ImageLocation));
            }

            foreach (Item item in Websites)
            {
                thumbnails.Add(Path.GetFileName(item.ImageLocation));

                if (item is TVShow tvshow)
                    thumbnails.Add(Path.GetFileName(tvshow.DetailsImageLocation));
            }

            try
            {
                string[] files = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "thumbnails/"));

                foreach (string file in files)
                {
                    bool a = thumbnails.Contains(Path.GetFileName(file));
                    bool b = uifiles.Contains(Path.GetFileName(file));

                    if (!a && !b)
                    {
                        File.Delete(file);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Couldn't complete request.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OpenGameDirectory(Item item)
        {
            if (item is VideoGame game)
            {
                try
                {
                    Process.Start(Path.GetDirectoryName(game.GameLocation));
                }
                catch
                {
                    MessageBox.Show("Couldn't open directory. Make sure it points to a valid path.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void RemoveEvent(Event evnt)
        {
            Events.Remove(evnt);

            if (!UnsavedChanges)
                UnsavedChanges = true;

            RaisePropertyChanged(nameof(EventHeader));
            RaisePropertyChanged(nameof(EventsSorted));
        }

        #endregion
    }
}
