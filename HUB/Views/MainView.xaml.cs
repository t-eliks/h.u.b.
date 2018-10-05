namespace HUB.Views
{
    using HUB.ViewModels;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            foreach (Control ctrl in menuGrid.Children)
            {
                if (ctrl is Button btn)
                {
                    btn.Style = (Style)this.Resources["MenuButton"];
                }
            }

            foreach (var ctrl in menuContainer.Children)
            {
                if (ctrl is Grid ctl)
                {
                    ctl.Visibility = Visibility.Collapsed;
                }
            }

            ((Button)e.Source).Style = (Style)this.Resources["SelectedButton"];

            switch (index)
            {
                case 0:
                    Panel0.Visibility = Visibility.Visible;
                    break;
                case 1:
                    Panel1.Visibility = Visibility.Visible;
                    break;
                case 2:
                    Panel2.Visibility = Visibility.Visible;
                    break;
                case 3:
                    Panel3.Visibility = Visibility.Visible;
                    break;
                default:
                    Panel0.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Button_Clicker(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Command.Execute(null);
        }

    }
}
