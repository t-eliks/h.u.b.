namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for ButtonDock.xaml
    /// </summary>
    public partial class ButtonDockTemplate : UserControl
    {
        public event RoutedEventHandler ButtonClicked;

        public ButtonDockTemplate()
        {
            InitializeComponent();

            btn.Click += (s, e) => this.ButtonClicked?.Invoke(btn, e);
        }
    }
}
