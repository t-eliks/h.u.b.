namespace HUB.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Media.Imaging;

    using HUB.Logic;

    [Serializable]
    public class Item : ISerializable, INotifyPropertyChanged
    {
        #region Constructors

        public Item()
        {
            Title = null;
            ImageLocation = null;
            EdgeColor = null;
            MiddleColor = null;
            HighlightColor = null;
        }

        public Item(string title, string imagelocation, string edgecolor, string middlecolor, string highlightcolor)
        {
            Title = title;
            ImageLocation = imagelocation;
            EdgeColor = edgecolor;
            MiddleColor = middlecolor;
            HighlightColor = highlightcolor;

            ContextMenuMap = new ObservableCollection<ContextMenuItem>();
        }

        #endregion

        #region Fields and Properties

        private string _Title;

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (_Title != value)
                {
                    _Title = value;

                    RaisePropertyChanged(nameof(Title));
                }
            }
        }

        private string _ImageLocation;

        public string ImageLocation
        {
            get
            {
                return _ImageLocation;
            }
            set
            {
                if (_ImageLocation != value)
                {
                    _ImageLocation = value;

                    RaisePropertyChanged(nameof(ImageLocation));
                    RaisePropertyChanged(nameof(DisplayImage));
                }
            }
        }

        public BitmapSource DisplayImage
        {
            get { return IconHandler.GetBitmapFromLocal(ImageLocation, 350, 230); }
        }

        private string _EdgeColor;

        public string EdgeColor
        {
            get
            {
                return _EdgeColor;
            }
            set
            {
                if (_EdgeColor != value)
                {
                    _EdgeColor = value;

                    RaisePropertyChanged(nameof(EdgeColor));
                }
            }
        }

        private string _MiddleColor;

        public string MiddleColor
        {
            get
            {
                return _MiddleColor;
            }
            set
            {
                if (_MiddleColor != value)
                {
                    _MiddleColor = value;

                    RaisePropertyChanged(nameof(MiddleColor));
                }
            }
        }

        private string _HighlightColor;

        public string HighlightColor
        {
            get
            {
                return _HighlightColor;
            }
            set
            {
                if (_HighlightColor != value)
                {
                    _HighlightColor = value;

                    RaisePropertyChanged(nameof(HighlightColor));
                }
            }
        }

        private bool _Filtered;

        public bool Filtered
        {
            get
            {
                return _Filtered;
            }
            set
            {
                _Filtered = value;

                RaisePropertyChanged(nameof(Filtered));
            }
        }

        private ObservableCollection<ContextMenuItem> _ContextMenuMap;

        public ObservableCollection<ContextMenuItem> ContextMenuMap
        {
            get
            {
                return _ContextMenuMap;
            }
            set
            {
                if (_ContextMenuMap != value)
                {
                    _ContextMenuMap = value;

                    RaisePropertyChanged(nameof(_ContextMenuMap));
                }
            }
        }

        #endregion

        #region Methods

        public void AddContextMenuPathCommand(string text, string path)
        {
            ContextMenuMap.Add(new ContextMenuItem(text, path));
        }

        #endregion

        #region Serialization

        protected Item(SerializationInfo info, StreamingContext context)
        {
            try
            {
                Title = (string)info.GetValue("title", typeof(string));
                ImageLocation = (string)info.GetValue("imagelocation", typeof(string));
                EdgeColor = (string)info.GetValue("edgecolor", typeof(string));
                MiddleColor = (string)info.GetValue("middlecolor", typeof(string));
                HighlightColor = (string)info.GetValue("highlightcolor", typeof(string));
                Filtered = (bool)info.GetValue("filtered", typeof(bool));
                try
                {
                    ContextMenuMap = (ObservableCollection<ContextMenuItem>)info.GetValue("contextmenumap", typeof(ObservableCollection<ContextMenuItem>));
                }
                catch
                {
                    ContextMenuMap = new ObservableCollection<ContextMenuItem>();
                }
            }
            catch
            {
                Title = null; 
                ImageLocation = null;
                EdgeColor = null;
                MiddleColor = null;
                HighlightColor = null;
                Filtered = false;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("title", Title);
            info.AddValue("imagelocation", ImageLocation);
            info.AddValue("edgecolor", EdgeColor);
            info.AddValue("middlecolor", MiddleColor);
            info.AddValue("highlightcolor", HighlightColor);
            info.AddValue("filtered", Filtered);
            info.AddValue("contextmenumap", ContextMenuMap);
        }

        #endregion

        #region INotifyPropertyChanged Impl.

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}
