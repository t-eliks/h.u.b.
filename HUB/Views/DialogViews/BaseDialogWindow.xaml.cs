namespace HUB.Views.DialogViews
{
    using HUB.Interfaces;
    using System.Windows;

    public partial class BaseDialogWindow : Window, IDialogWindow
    {
        public BaseDialogWindow()
        {
            InitializeComponent();
        }
    }
}
