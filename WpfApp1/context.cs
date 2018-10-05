using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class context : INotifyPropertyChanged
    {
        private ObservableCollection<Game> _collection;

        public ObservableCollection<Game> collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
                RaisePropertyChanged("collection");
            }
        }

        public context()
        {
            collection = new ObservableCollection<Game> { new Game("Wow"), new Game("GOT") };
        }

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
