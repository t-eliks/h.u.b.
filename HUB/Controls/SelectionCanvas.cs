namespace HUB.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    public partial class SelectionCanvas : Canvas
    {
        public SelectionCanvas()
        {

        }

        #region Fields and (Dependency) Properties

        Point point;
        Point secondpoint;
        Rectangle rectangle;
        
        double SWidth;
        double SHeight;

        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("Image",
            typeof(BitmapSource), typeof(SelectionCanvas), new FrameworkPropertyMetadata(null, OnImagePropertyChanged));

        public BitmapSource Image
        {
            get { return (BitmapSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        private static void OnImagePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            SelectionCanvas canvas = (SelectionCanvas)source;
            BitmapSource image = (BitmapSource)e.NewValue;

            canvas.Width = image.PixelWidth;
            canvas.Height = image.PixelHeight;
            canvas.SWidth = image.PixelWidth;
            canvas.SHeight = image.PixelHeight;

            canvas.ResizePercentage = 100;

            canvas.Background = new ImageBrush()
            {
                ImageSource = image
            };
        }

        public static readonly DependencyProperty CroppedImageProperty = DependencyProperty.Register("CroppedImage",
            typeof(ImageSource), typeof(SelectionCanvas), new FrameworkPropertyMetadata(null));

        public ImageSource CroppedImage
        {
            get { return (ImageSource)GetValue(CroppedImageProperty); }
            set { SetValue(CroppedImageProperty, value); }
        }

        public static readonly DependencyProperty ResizePercentageProperty = DependencyProperty.Register("ResizePercentage", 
            typeof(double), typeof(SelectionCanvas), new FrameworkPropertyMetadata(100.0, OnResizePercentagePropertyChanged));

        public double ResizePercentage
        {
            get { return (double)GetValue(ResizePercentageProperty); }
            set { SetValue(ResizePercentageProperty, value); }
        }

        private static void OnResizePercentagePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            SelectionCanvas canvas = (SelectionCanvas)source;
            double resizepercentage = (double)e.NewValue;

            canvas.Width = canvas.SWidth * (resizepercentage / 100);
            canvas.Height = canvas.SHeight * (resizepercentage / 100);
        }

        #endregion

        #region Methods

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (!this.IsMouseCaptured)
            {
                if (rectangle != null)
                {
                    this.Children.Remove(rectangle);
                    rectangle = null;
                }
                point = e.GetPosition(this);
                this.CaptureMouse();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsMouseCaptured)
            {
                secondpoint = e.GetPosition(this);

                if (rectangle == null)
                {
                    rectangle = new Rectangle()
                    {
                        Stroke = new SolidColorBrush() { Color = Colors.Red },
                        StrokeThickness = 5,
                        Fill = new SolidColorBrush() { Color = Colors.Transparent },
                    };

                    this.Children.Add(rectangle);
                }

                rectangle.Width = Math.Abs(secondpoint.X - point.X);
                rectangle.Height = Math.Abs(secondpoint.Y - point.Y);

                Canvas.SetTop(rectangle, secondpoint.Y > point.Y ? point.Y : secondpoint.Y);
                Canvas.SetLeft(rectangle, secondpoint.X > point.X ? point.X : secondpoint.X);
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (this.IsMouseCaptured)
            {
                this.ReleaseMouseCapture();

                try
                {
                    var bmp = new BitmapImage(new Uri(((ImageBrush)(this.Background)).ImageSource.ToString()));

                    var xCorner = secondpoint.X > point.X ? point.X : secondpoint.X;
                    var yCorner = secondpoint.Y > point.Y ? point.Y : secondpoint.Y;

                    var x = (int)(xCorner / (ResizePercentage / 100));
                    var y = (int)(yCorner / (ResizePercentage / 100));

                    var width = (int)(rectangle.Width / (ResizePercentage / 100));
                    var height = (int)(rectangle.Height / (ResizePercentage / 100));

                    var img = new CroppedBitmap(bmp, new Int32Rect(x, y, width, height));

                    CroppedImage = img;
                }
                catch
                {
                    if (rectangle != null)
                    {
                        this.Children.Remove(rectangle);
                        rectangle = null;
                    }
                    MessageBox.Show("Something went wrong! Avoid going out of bounds or clicking without dragging.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        #endregion
    }
}
