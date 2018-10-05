namespace HUB.Views.Blend
{
    using HUB.ViewModels;
    using System.Windows;


    /// <summary>
    /// Interaction logic for MainView_Blend.xaml
    /// </summary>
    public partial class MainView_Blend : Window
    {
        public MainView_Blend()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
