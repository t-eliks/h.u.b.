namespace HUB.ViewModels.DialogViewModels
{
    using System;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using HUB.Interfaces;
    using HUB.Logic;
    using HUB.Views.DialogViewModels;

    public class ImageCropDialogViewModel : DialogViewModelBase<CroppedBitmap>
    {
        public ImageCropDialogViewModel()
        {
            CancelCommand = new RelayCommand<IDialogWindow>(Cancel);
            SaveCommand = new RelayCommand<IDialogWindow>(Save);
        }

        #region ICommands

        public ICommand CancelCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        #endregion

        #region Fields and Properties

        private ImageSource _Image;
        
        public ImageSource Image
        {
            get
            {
                return _Image;
            }
            set
            {
                if (_Image != value)
                {
                    _Image = value;

                    CroppedImage = new CroppedBitmap(new BitmapImage(new Uri(Image.ToString())), new System.Windows.Int32Rect());

                    RaisePropertyChanged(nameof(Image));
                }
            }
        }

        private CroppedBitmap _CroppedImage;

        public CroppedBitmap CroppedImage
        {
            get
            {
                return _CroppedImage;
            }
            set
            {
                if (_CroppedImage != value)
                {
                    _CroppedImage = value;

                    CroppedImageBrush = new ImageBrush() { ImageSource = value };

                    RaisePropertyChanged(nameof(CroppedImage));
                }
            }
        }

        private ImageBrush _CroppedImageBrush;

        public ImageBrush CroppedImageBrush
        {
            get
            {
                if (_CroppedImageBrush == null)
                    return new ImageBrush() { ImageSource = Image };

                return _CroppedImageBrush;
            }
            set
            {
                if (_CroppedImageBrush != value)
                {
                    _CroppedImageBrush = value;

                    RaisePropertyChanged(nameof(CroppedImageBrush));
                }
            }
        }

        #endregion

        #region Methods

        private void Cancel(IDialogWindow window)
        {
            CloseDialogWithResult(window, null);
        }

        private void Save(IDialogWindow window)
        {
            CloseDialogWithResult(window, CroppedImage);
        }

        #endregion
    }
}
