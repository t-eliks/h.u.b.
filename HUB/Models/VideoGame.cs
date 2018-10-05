namespace HUB.Models
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class VideoGame : Item
    {
        #region Constructors

        public VideoGame()
        {
            Title = null;
            ImageLocation = null;
            EdgeColor = null;
            MiddleColor = null;
            HighlightColor = null;
            GameLocation = null;
            TargetParameters = null;
            Installed = false;
        }

        public VideoGame(string title, string imagelocation, string edgecolor, string middlecolor, string highlightcolor, string gamelocation, string targetparameters, bool installed)
            : base(title, imagelocation, edgecolor, middlecolor, highlightcolor)
        {
            Title = title;
            ImageLocation = imagelocation;
            EdgeColor = edgecolor;
            MiddleColor = middlecolor;
            HighlightColor = highlightcolor;
            GameLocation = gamelocation;
            TargetParameters = targetparameters;
            Installed = installed;
        }

        #endregion

        #region Properties and Fields

        private string _GameLocation;

        public string GameLocation
        {
            get
            {
                return _GameLocation;
            }
            set
            {
                if (_GameLocation != value)
                {
                    _GameLocation = value;

                    RaisePropertyChanged(nameof(GameLocation));
                }
            }
        }

        private string _TargetParameters;

        public string TargetParameters
        {
            get
            {
                return _TargetParameters;
            }
            set
            {
                if (_TargetParameters != value)
                {
                    _TargetParameters = value;

                    RaisePropertyChanged(nameof(TargetParameters));
                }
            }
        }

        private bool _Installed;

        public bool Installed
        {
            get
            {
                return _Installed;
            }
            set
            {
                if (_Installed != value)
                {
                    _Installed = value;

                    if (value == false)
                    {
                        var cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                        if (bool.Parse(cfg.AppSettings.Settings["uninstalledgamesfiltered"].Value))
                        {
                            Filtered = true;
                        }
                    }


                    RaisePropertyChanged(nameof(Installed));
                }
            }
        }

        #endregion

        #region Serialization

        protected VideoGame(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            try
            {
                GameLocation = info.GetString("gamelocation");
                TargetParameters = info.GetString("targetparameters");
                Installed = info.GetBoolean("installed");
            }
            catch
            {
                GameLocation = null;
                TargetParameters = null;
                Installed = false;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("gamelocation", GameLocation);
            info.AddValue("targetparameters", TargetParameters);
            info.AddValue("installed", Installed);
        }

        #endregion
    }
}