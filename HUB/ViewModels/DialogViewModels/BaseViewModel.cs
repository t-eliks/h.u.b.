namespace HUB.ViewModels.DialogViewModels
{
    using System.ComponentModel;

    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel() { }

        #region INotifyPropertyChanged Impl.

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion
    }
}
