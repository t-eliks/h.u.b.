namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for CheckBoxTextBlockTemplate.xaml
    /// </summary>
    public partial class CheckBoxTextBlockTemplate : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text",
            typeof(string), typeof(CheckBoxTextBlockTemplate));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty CheckBoxIsCheckedProperty = DependencyProperty.Register("CheckBoxIsChecked",
            typeof(bool), typeof(CheckBoxTextBlockTemplate));

        public bool CheckBoxIsChecked
        {
            get { return (bool)GetValue(CheckBoxIsCheckedProperty); }
            set { SetValue(CheckBoxIsCheckedProperty, value); }
        }

        public CheckBoxTextBlockTemplate()
        {
            InitializeComponent();
        }
    }
}
