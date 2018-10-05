namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TextBlockTextBoxTemplate.xaml
    /// </summary>
    public partial class TextBlockTextBoxTemplate : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(TextBlockTextBoxTemplate));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextBoxTextProperty = DependencyProperty.Register("TextBoxText",
            typeof(string), typeof(TextBlockTextBoxTemplate));

        public string TextBoxText
        {
            get { return (string)GetValue(TextBoxTextProperty); }
            set { SetValue(TextBoxTextProperty, value); }
        }

        public TextBlockTextBoxTemplate()
        {
            InitializeComponent();
        }
    }
}
