namespace HUB.ViewModels.DialogViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Imaging;

    using HUB.Interfaces;
    using HUB.Logic;
    using HUB.Logic.Dialog;
    using HUB.Models;
    using HUB.Views.DialogViewModels;

    public class AddGameDialogViewModel : AddEditObjectViewModelBase
    {
        public AddGameDialogViewModel()
        {
            ChooseGameLocationCommand = new RelayCommand(ChooseGameLocation);
            CloseCommand = new RelayCommand<string>(AddItem);
        }

        #region ICommands

        public ICommand ChooseGameLocationCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        #endregion

        #region Fields and Properties

        public new event RoutedEventHandler OnItemAdded;

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

                    if (EditMode)
                        ((VideoGame)Item).GameLocation = value;

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

                    if (EditMode)
                        ((VideoGame)Item).TargetParameters = value;

                    RaisePropertyChanged(nameof(TargetParameters));
                }
            }
        }

        #endregion

        #region Methods

        public void ChooseGameLocation()
        {
            string path = FileSelectHandler.ChooseGameLocation();

            if (path != null)
            {
                GameLocation = path;

                Title = Path.GetFileNameWithoutExtension(path);
            }
        }

        public override void AddItem(string result)
        {
            if (!EditMode && bool.Parse(result))
            {
                if (!String.IsNullOrEmpty(GameLocation))
                    ContextMenuMap.Add(new ContextMenuItem("Open Directory", Path.GetDirectoryName(GameLocation)));

                var game = new VideoGame(Title, Path.GetFileName(ImageLocation), EdgeColor.ToString(), MiddleColor.ToString(), HighlightColor.ToString(), GameLocation, TargetParameters, true);

                game.ContextMenuMap = this.ContextMenuMap;

                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = game });
            }
            else
            {
                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = null });
            }
        }

        #endregion
    }
}
