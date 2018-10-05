namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ShadowedTextBox.xaml
    /// </summary>
    public partial class ShadowedTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(ShadowedTextBox));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public new static readonly DependencyProperty IsEnabledProperty = DependencyProperty.Register("IsEnabled",
            typeof(bool), typeof(ShadowedTextBox), new FrameworkPropertyMetadata(true));

        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public ShadowedTextBox()
        {
            InitializeComponent();
        }
    }
}
