namespace HUB.Logic.Dialog
{
    using HUB.Views.DialogViewModels;
    using HUB.Interfaces;
    using HUB.Views.DialogViews;

    public class DialogService
    {
        public T OpenDialog<T>(DialogViewModelBase<T> viewModel)
        {
            IDialogWindow window = new BaseDialogWindow();
            window.DataContext = viewModel;
            window.ShowDialog();
            return viewModel.DialogResult;
        }
    }
}
