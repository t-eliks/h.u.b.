namespace HUB.ViewModels.DialogViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using HUB.Logic;
    using HUB.Logic.Dialog;
    using HUB.Models;

    public class AddEditObjectViewModelBase : BaseViewModel, INotifyPropertyChanged
    {
        public AddEditObjectViewModelBase()
        {
            DisplayImage = new BitmapImage(new Uri("pack://application:,,,/HUB;component/icons/noimage.png"));
            ContextMenuMap = new ObservableCollection<ContextMenuItem>();

            AddItemCommand = new RelayCommand<string>(AddItem);
            ChooseImageLocationCommand = new RelayCommand(ChooseImageLocation);
            AddContextItemCommand = new RelayCommand(AddContextItem);
            RemoveContextItemCommand = new RelayCommand<ContextMenuItem>(RemoveContextItem);
        }

        #region ICommands

        public ICommand AddItemCommand { get; private set; }

        public ICommand ChooseImageLocationCommand { get; private set; }

        public ICommand AddContextItemCommand { get; private set; }

        public ICommand RemoveContextItemCommand { get; private set; }

        #endregion

        #region Properties and Fields

        public event RoutedEventHandler OnItemAdded;

        public bool EditMode { get; set; }

        private Item _Item;

        public Item Item
        {
            get
            {
                return _Item;
            }
            set
            {
                if (_Item != value)
                {
                    _Item = value;

                    RaisePropertyChanged(nameof(Item));
                }
            }
        }

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

                    if (EditMode)
                        Item.Title = value;

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

                    if (EditMode)
                    {
                        Item.ImageLocation = Path.GetFileName(value);
                        DisplayImage = Item.DisplayImage;
                    }
                    else if (value != null)
                        DisplayImage = new BitmapImage(new Uri(value));

                    RaisePropertyChanged(nameof(ImageLocation));
                }
            }
        }

        private BitmapSource _DisplayImage;

        public BitmapSource DisplayImage
        {
            get
            {
                return _DisplayImage;
            }
            set
            {
                if (_DisplayImage != value)
                {
                    _DisplayImage = value;

                    RaisePropertyChanged(nameof(DisplayImage));
                }
            }
        }

        private string _EdgeColor = "#ff1e1f26";

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

                    if (EditMode)
                        Item.EdgeColor = value;

                    RaisePropertyChanged(nameof(EdgeColor));
                }
            }
        }

        private string _MiddleColor = "#ff283655";

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

                    if (EditMode)
                        Item.MiddleColor = value;

                    RaisePropertyChanged(nameof(MiddleColor));
                }
            }
        }

        private string _HighlightColor = "#ff4d648d";

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

                    if (EditMode)
                        Item.HighlightColor = value;

                    RaisePropertyChanged(nameof(HighlightColor));
                }
            }
        }

        public string ContextItemText { get; set; }

        public string ContextItemPath { get; set; }

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

                    if (EditMode)
                        Item.ContextMenuMap = value;

                    RaisePropertyChanged(nameof(ContextMenuMap));
                }
            }
        }

        #endregion

        #region Methods

        public void ChooseImageLocation()
        {
            string path = FileSelectHandler.ChooseImageLocation();

            if (path != null)
            {
                ImageLocation = path;
            }
        }

        public void AddContextItem()
        {
            if (!String.IsNullOrWhiteSpace(ContextItemPath) && !String.IsNullOrWhiteSpace(ContextItemText))
            {
                ContextMenuMap.Add(new ContextMenuItem(ContextItemText, ContextItemPath));
            }

        }

        public virtual void AddItem(string result)
        {
            if (!EditMode && bool.Parse(result))
            {
                var item = new Item(Title?.ToUpper(), Path.GetFileName(ImageLocation), EdgeColor.ToString(), MiddleColor.ToString(), HighlightColor.ToString());

                item.ContextMenuMap = this.ContextMenuMap;

                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = Item });
            }
            else
            {
                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = null });
            }
        }

        private void RemoveContextItem(ContextMenuItem contextitem)
        {
            if (contextitem != null)
                Item.ContextMenuMap.Remove(contextitem);
        }

        #endregion
    }
}
