namespace HUB.Models
{
    using HUB.Logic;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Windows;
    using System.Windows.Input;

    [Serializable]
    public class ContextMenuItem : ISerializable, INotifyPropertyChanged
    {
        public ContextMenuItem(string text, string path)
        {
            Text = text;
            Path = path;
            Command = new RelayCommand(delegate
            {
                try
                {
                    Process.Start(path);
                }
                catch
                {
                    MessageBox.Show("Couldn't open link/folder/application. Make sure path is valid", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            });
        }

        private string _Text;

        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;

                    RaisePropertyChanged(nameof(Text));
                }
            }
        }

        private string _Path;

        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                if (_Path != value)
                {
                    _Path = value;

                    RaisePropertyChanged(nameof(Path));
                }
            }
        }
            
        public ICommand Command { get; set; }

        #region Serialization

        protected ContextMenuItem(SerializationInfo info, StreamingContext context)
        {
            try
            {
                Text = info.GetString("text");
                Path = info.GetString("path");
                Command = new RelayCommand(delegate
                {
                    try
                    {
                        Process.Start(Path);
                    }
                    catch
                    {
                        MessageBox.Show("Couldn't open link/folder/application. Make sure path is valid", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                });
            }
            catch
            {
                Text = null;
                Path = null;
                Command = null;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("text", Text);
            info.AddValue("path", Path);
        }

        #endregion

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
