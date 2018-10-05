namespace HUB.Logic
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;

    using HUB.ViewModels;
    using HUB.ViewModels.DialogViewModels;

    public class FileSelectHandler
    {
        public static string ChooseGameLocation()
        {
            OpenFileDialog file = new OpenFileDialog();

            file.InitialDirectory = Directory.GetCurrentDirectory();

            file.Title = "Pick Game Launcher Location";

            file.Filter = "Exectuble file (*.exe)|*.exe";

            bool? result = file.ShowDialog();

            if (result != null && result == true)
            {
                return file.FileName;
            }
            return null;
        }

        public static string ChooseImageLocation()
        {
            OpenFileDialog file = new OpenFileDialog();

            file.InitialDirectory = Directory.GetCurrentDirectory();

            file.Title = "Select a Display Image";

            file.Filter = "All supported graphics|*.jpg;*.jpeg;*.png";

            bool? result;

            result = file.ShowDialog();

            if (result != null && result == true)
            {
                var dialog = new ImageCropDialogViewModel();
                dialog.Image = new BitmapImage(new Uri(file.FileName));

                var croppedimage = MainViewModel._dialogService.OpenDialog(dialog);

                if (result != null)
                {
                    return IconHandler.SaveBitmapToLocalStorage(croppedimage);
                }
                else
                    return IconHandler.SaveImageToLocalStorage(file.FileName);
            }
            return null;
        }
    }
}
