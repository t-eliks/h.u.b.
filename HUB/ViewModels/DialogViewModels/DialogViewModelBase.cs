namespace HUB.Views.DialogViewModels
{
    using HUB.Interfaces;
    using HUB.ViewModels.DialogViewModels;

    public abstract class DialogViewModelBase<T> : BaseViewModel
    {
        public T DialogResult { get; set; }

        public void CloseDialogWithResult(IDialogWindow dialog, T result)
        {
            DialogResult = result;

            if (dialog != null)
                dialog.DialogResult = true;
        }
    }
}
