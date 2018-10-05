namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TitleBlockTemplate.xaml
    /// </summary>
    public partial class AddEditTitleBlockTemplate : UserControl
    {
        public event RoutedEventHandler OnButtonClicked;

        public AddEditTitleBlockTemplate()
        {
            InitializeComponent();

            Button.Click += (s, e) => this.OnButtonClicked?.Invoke(Button, e);
        }
    }
}
