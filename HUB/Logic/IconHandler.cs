namespace HUB.Logic
{
    using System;
    using System.IO;
    using System.Windows.Media.Imaging;
    using System.Windows;

    internal class IconHandler
    {
        private static string directory { get; set; } = Directory.GetCurrentDirectory() + "\\thumbnails\\";

        public static string SaveImageToLocalStorage(string imagepath)
        {
            if (!String.IsNullOrEmpty(imagepath))
            {
                try
                {
                    BitmapImage image = new BitmapImage(new Uri(imagepath));

                    if (!Directory.Exists(directory))
                        Directory.CreateDirectory(directory);

                    var path = directory + $@"{Guid.NewGuid()}.png";

                    File.Copy(imagepath, path, true);

                    return path;
                }
                catch
                {
                    MessageBox.Show("Error occured.");
                }
            }
            return null;
        }

        public static string SaveBitmapToLocalStorage(BitmapSource bmp)
        {
            if (bmp != null)
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var path = directory + $@"{Guid.NewGuid()}.png";
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bmp));
                    encoder.Save(stream);
                    return path;
                }
            }
            return null;
        }

        public static BitmapSource GetBitmapFromLocal(string imagepath, int decodepixelwidth, int decodepixelheight)
        {
            try
            {
                var bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = new Uri(directory + imagepath);
                bmp.DecodePixelWidth = decodepixelwidth;
                bmp.DecodePixelHeight = decodepixelheight;
                bmp.EndInit();

                if (!String.IsNullOrEmpty(imagepath))
                {
                    return bmp;
                }
            }
            catch
            {
                return (BitmapImage)Application.Current.FindResource("DefaultImage");
            }

            return null;
        }
    }
}
