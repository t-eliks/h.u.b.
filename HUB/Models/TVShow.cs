namespace HUB.Models
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class TVShow : Website
    {
        #region Constructors

        public TVShow()
        {
            Title = null;
            ImageLocation = null;
            EdgeColor = null;
            MiddleColor = null;
            HighlightColor = null;
            Description = null;
            Link = null;
            CurrentEpisode = null;
            EpisodesInSeason = null;
            CurrentSeason = null;
            ConfirmedSeasons = null;
            NextSeasonOutDate = null;
        }

        public TVShow(string title, string imagelocation, string edgecolor, string middlecolor, string highlightcolor, string description, string link,
            int? currentepisode, int? episodesinseason, int? currentseason, int? confirmedseasons, DateTime? nextseasonoutdate) :
            base(title, imagelocation, edgecolor, middlecolor, highlightcolor, description, link)
        {
            Title = title;
            ImageLocation = imagelocation;
            EdgeColor = edgecolor;
            MiddleColor = middlecolor;
            HighlightColor = highlightcolor;
            Description = description;
            Link = link;
            CurrentEpisode = currentepisode;
            EpisodesInSeason = episodesinseason;
            CurrentSeason = currentseason;
            ConfirmedSeasons = confirmedseasons;
            NextSeasonOutDate = nextseasonoutdate;
        }

        #endregion

        #region Fields and Properties

        private string _DetailsImageLocation;

        public string DetailsImageLocation
        {
            get
            {
                return _DetailsImageLocation;
            }
            set
            {
                if (_DetailsImageLocation != value)
                {
                    _DetailsImageLocation = value;

                    RaisePropertyChanged(nameof(DetailsImageLocation));
                }
            }
        }

        private int? _CurrentEpisode;

        public int? CurrentEpisode
        {
            get
            {
                return _CurrentEpisode;
            }
            set
            {
                if (_CurrentEpisode != value)
                {
                    _CurrentEpisode = value;

                    RaisePropertyChanged(nameof(CurrentEpisode));
                }
            }
        }

        private int? _EpisodesInSeason;

        public int? EpisodesInSeason
        {
            get
            {
                return _EpisodesInSeason;
            }
            set
            {
                if (_EpisodesInSeason != value)
                {
                    _EpisodesInSeason = value;

                    RaisePropertyChanged(nameof(EpisodesInSeason));
                }
            }
        }

        private int? _CurrentSeason;

        public int? CurrentSeason
        {
            get
            {
                return _CurrentSeason;
            }
            set
            {
                if (_CurrentSeason != value)
                {
                    _CurrentSeason = value;

                    RaisePropertyChanged(nameof(CurrentSeason));
                }
            }
        }

        private int? _ConfirmedSeasons;

        public int? ConfirmedSeasons
        {
            get
            {
                return _ConfirmedSeasons;
            }
            set
            {
                if (_ConfirmedSeasons != value)
                {
                    _ConfirmedSeasons = value;

                    RaisePropertyChanged(nameof(ConfirmedSeasons));
                }
            }
        }

        private DateTime? _NextSeasonOutDate;

        public DateTime? NextSeasonOutDate
        {
            get
            {
                return _NextSeasonOutDate;
            }
            set
            {
                if (_NextSeasonOutDate != value)
                {
                    _NextSeasonOutDate = value;

                    RaisePropertyChanged(nameof(NextSeasonOutDate));
                }
            }
        }

        #endregion

        #region Serialization

        protected TVShow(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            try
            {
                CurrentEpisode = info.GetInt32("currentepisode");
                EpisodesInSeason = info.GetInt32("episodesinseason");
                CurrentSeason = info.GetInt32("currentseason");
                ConfirmedSeasons = info.GetInt32("confirmedseasons");
                NextSeasonOutDate = info.GetDateTime("nextseasonoutdate");
                DetailsImageLocation = info.GetString("detailsimagelocation");
            }
            catch
            {
                CurrentEpisode = null;
                EpisodesInSeason = null;
                CurrentSeason = null;
                ConfirmedSeasons = null;
                NextSeasonOutDate = null;
                DetailsImageLocation = null;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("currentepisode", CurrentEpisode);
            info.AddValue("episodesinseason", EpisodesInSeason);
            info.AddValue("currentseason", CurrentSeason);
            info.AddValue("confirmedseasons", ConfirmedSeasons);
            info.AddValue("nextseasonoutdate", NextSeasonOutDate);
            info.AddValue("detailsimagelocation", DetailsImageLocation);
        }

        #endregion
        
    }
}
