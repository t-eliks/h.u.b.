namespace HUB.ViewModels.DialogViewModels
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using HUB.Logic;
    using HUB.Models;

    public class WebsiteDetailsViewModel : BaseViewModel
    {
        public WebsiteDetailsViewModel()
        {
            DecreaseEpisodeCommand = new RelayCommand(DecreaseEpisode);
            IncreaseEpisodeCommand = new RelayCommand(IncreaseEpisode);
            ChooseDetailsImageCommand = new RelayCommand(ChooseDetailsImage);
            OpenCommand = new RelayCommand(Open);
            CloseCommand = new RelayCommand(Close);
        }

        #region ICommands

        public ICommand DecreaseEpisodeCommand { get; private set; }

        public ICommand IncreaseEpisodeCommand { get; private set; }

        public ICommand ChooseDetailsImageCommand { get; private set; }

        public ICommand OpenCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }


        #endregion

        public event RoutedEventHandler OnClose;

        #region Properties and Fields

        private TVShow _Website;

        public TVShow Website
        {
            get
            {
                return _Website;
            }
            set
            {
                if (_Website != value)
                {
                    _Website = value;
                    RaisePropertyChanged(nameof(Website));
                }
            }
        }

        public string ImageLocation
        {
            get
            {
                return Website?.ImageLocation;
            }
            set
            {
                if (Website?.ImageLocation != value)
                {
                    Website.ImageLocation = value;
                    RaisePropertyChanged(nameof(ImageLocation));
                    RaisePropertyChanged(nameof(DisplayImage));
                }
            }
        }

        public BitmapSource DisplayImage
        {
            get
            {
                return Website?.DisplayImage;
            }
        }

        public string Title
        {
            get
            {
                return Website?.Title;
            }
            set
            {
                if (Website?.Title != value)
                {
                    Website.Title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        public string Link
        {
            get
            {
                return Website?.Link;
            }
            set
            {
                if (Website?.Link != value)
                {
                    Website.Link = value;
                    RaisePropertyChanged(nameof(Link));
                }
            }
        }

        public string Description
        {
            get
            {
                return Website?.Description;
            }
            set
            {
                if (Website?.Description != value)
                {
                    Website.Description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        public int? CurrentEpisode
        {
            get
            {
                return Website?.CurrentEpisode;
            }
            set
            {
                if (Website?.CurrentEpisode != value)
                {
                    Website.CurrentEpisode = value;
                    RaisePropertyChanged(nameof(CurrentEpisode));
                }
            }
        }

        public int? EpisodesInSeason
        {
            get
            {
                return Website?.EpisodesInSeason;
            }
            set
            {
                if (Website?.EpisodesInSeason != value)
                {
                    Website.EpisodesInSeason = value;
                    RaisePropertyChanged(nameof(EpisodesInSeason));
                }
            }
        }

        public int? CurrentSeason
        {
            get
            {
                return Website?.CurrentSeason;
            }
            set
            {
                if (Website?.CurrentSeason != value)
                {
                    Website.CurrentSeason = value;
                    RaisePropertyChanged(nameof(CurrentSeason));
                }
            }
        }

        public int? ConfirmedSeasons
        {
            get
            {
                return Website?.ConfirmedSeasons;
            }
            set
            {
                if (Website?.ConfirmedSeasons != value)
                {
                    Website.ConfirmedSeasons = value;
                    RaisePropertyChanged(nameof(ConfirmedSeasons));
                }
            }
        }

        public DateTime? NextSeasonOutDate
        {
            get
            {
                return Website?.NextSeasonOutDate;
            }
            set
            {
                if (Website?.NextSeasonOutDate != value)
                {
                    Website.NextSeasonOutDate = value;
                    RaisePropertyChanged(nameof(NextSeasonOutDate));
                }
            }
        }

        public string DetailsImageLocation
        {
            get
            {
                return Website?.DetailsImageLocation;
            }
            set
            {
                if (Website?.DetailsImageLocation != value)
                {
                    Website.DetailsImageLocation = Path.GetFileName(value);
                    RaisePropertyChanged(nameof(DetailsImageLocation));
                    RaisePropertyChanged(nameof(DetailsImage));
                }
            }
        }

        public BitmapSource DetailsImage { get { return DetailsImageLocation != null ? IconHandler.GetBitmapFromLocal(DetailsImageLocation, 1024, 1024) : null; } }

        #endregion

        #region Methods

        public void IncreaseEpisode()
        {
            if (CurrentEpisode < EpisodesInSeason)
            {
                CurrentEpisode += 1;
            }
        }

        public void DecreaseEpisode()
        {
            if (CurrentEpisode > 0)
            {
                CurrentEpisode -= 1;
            }
        }

        public void ChooseDetailsImage()
        {
            var path = FileSelectHandler.ChooseImageLocation();

            if (path != null)
            {
                DetailsImageLocation = path;
            }
        }

        public void Open()
        {
            try
            {
                System.Diagnostics.Process.Start(Website?.Link);
            }
            catch
            {
                MessageBox.Show("Something went wrong. Make sure path or link is correct", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public void Close()
        {
            OnClose?.Invoke(this, new RoutedEventArgs());
        }

        #endregion
    }
}
