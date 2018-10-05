namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ThreeColorPicksTemplate.xaml
    /// </summary>
    public partial class ThreeColorPicksTemplate : UserControl
    {
        public static readonly DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation",
            typeof(Orientation), typeof(ThreeColorPicksTemplate), new FrameworkPropertyMetadata(Orientation.Vertical));

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty EdgeColorProperty = DependencyProperty.Register("EdgeColor",
            typeof(string), typeof(ThreeColorPicksTemplate));

        public string EdgeColor
        {
            get { return (string)GetValue(EdgeColorProperty); }
            set { SetValue(EdgeColorProperty, value); }
        }

        public static readonly DependencyProperty MiddleColorProperty = DependencyProperty.Register("MiddleColor",
           typeof(string), typeof(ThreeColorPicksTemplate));

        public string MiddleColor
        {
            get { return (string)GetValue(MiddleColorProperty); }
            set { SetValue(MiddleColorProperty, value); }
        }

        public static readonly DependencyProperty HighlightColorProperty = DependencyProperty.Register("HighlightColor",
          typeof(string), typeof(ThreeColorPicksTemplate));

        public string HighlightColor
        {
            get { return (string)GetValue(HighlightColorProperty); }
            set { SetValue(HighlightColorProperty, value); }
        }

        public ThreeColorPicksTemplate()
        {
            InitializeComponent();
        }
    }
}
