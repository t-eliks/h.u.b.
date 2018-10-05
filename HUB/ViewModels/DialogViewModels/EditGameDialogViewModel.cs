using HUB.Interfaces;
using HUB.Logic;
using HUB.Logic.Dialog;
using HUB.Models;
using HUB.Views.DialogViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace HUB.ViewModels.DialogViewModels
{
    public class EditGameDialogViewModel : DialogViewModelBase<DialogResults>, INotifyPropertyChanged
    {
        public EditGameDialogViewModel()
        {
            CloseCommand = new RelayCommand<IDialogWindow>(Close);
            ChooseGameLocationCommand = new RelayCommand(ChooseGameLocation);
            ChooseImageLocationCommand = new RelayCommand(ChooseImageLocation);
        }

        public ICommand CloseCommand { get; private set; }

        public ICommand ChooseGameLocationCommand { get; private set; }

        public ICommand ChooseImageLocationCommand { get; private set; }

        private VideoGame _Game;
        
        public VideoGame Game
        {
            get
            {
                return _Game;
            }
            set
            {
                _Game = value;
                RaisePropertyChanged("Game");
            }
        }

        public string GameLocation
        {
            get
            {
                return Game.GameLocation;
            }
            set
            {
                RaisePropertyChanged("GameLocation");
            }
        }

        public BitmapSource DisplayImage
        {
            get
            {
                return Game.DisplayImage;
            }
            set
            {
                RaisePropertyChanged("DisplayImage");
            }
        }

        public void Close(IDialogWindow window)
        {
            CloseDialogWithResult(window, DialogResults.Undefined);
        }

        public void ChooseGameLocation()
        {
            string path = FileSelectHandler.ChooseGameLocation();

            if (path != null)
            {
                Game.GameLocation = path;
                GameLocation = path;
            }
        }

        public void ChooseImageLocation()
        {
            string path = FileSelectHandler.ChooseImageLocation();

            if (path != null)
            {
                Game.ImageLocation = IconHandler.SaveImageToLocalStorage(path);
                DisplayImage = null;
            }
        }

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
