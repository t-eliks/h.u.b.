namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TextBlockButtonTemplate.xaml
    /// </summary>
    public partial class TextBlockButtonTemplate : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(TextBlockButtonTemplate));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ButtonContentProperty = DependencyProperty.Register("ButtonContent",
            typeof(string), typeof(TextBlockButtonTemplate));

        public string ButtonContent
        {
            get { return (string)GetValue(ButtonContentProperty); }
            set { SetValue(ButtonContentProperty, value); }
        }

        public static readonly DependencyProperty ButtonUidProperty = DependencyProperty.Register("ButtonUid",
            typeof(string), typeof(TextBlockButtonTemplate));

        public string ButtonUid
        {
            get { return (string)GetValue(ButtonUidProperty); }
            set { SetValue(ButtonUidProperty, value); }
        }

        public event RoutedEventHandler OnButtonClicked;

        public TextBlockButtonTemplate()
        {
            InitializeComponent();

            Button.Click += (s, e) => this.OnButtonClicked?.Invoke(Button, e);
        }
    }
}
