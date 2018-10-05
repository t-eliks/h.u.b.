namespace HUB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Windows.Media.Imaging;

    [Serializable]
    public class Event : INotifyPropertyChanged, ISerializable
    {
        public Event(string text, string subtext, DateTime date)
        {
            Text = text;
            Subtext = subtext;
            Date = date;
        }

        #region Properties and Fields

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

        private string _Subtext;

        public string Subtext
        {
            get
            {
                return _Subtext;
            }

            set
            {
                if (_Subtext != value)

                {
                    _Subtext = value;


                    RaisePropertyChanged(nameof(Subtext));
                }
            }
        }

        private DateTime _Date;

        public DateTime Date
        {
            get
            {
                return _Date;
            }
            set
            {
                if (_Date != value)
                {
                    _Date = value;

                    RaisePropertyChanged(nameof(Date));
                    RaisePropertyChanged(nameof(Remainder));
                }
            }
        }

        public string Remainder
        {
            get
            {
                TimeSpan span = Date.Subtract(DateTime.Now);

                if (span.Days >= 1)
                    return span.Days + " days left";
                else
                    return String.Format("{0} hours, {1} minutes left", span.Hours, span.Minutes);
            }
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

        #region Serialization

        protected Event(SerializationInfo info, StreamingContext context)
        {
            try
            {
                Text = (string)info.GetValue("text", typeof(string));
                Subtext = (string)info.GetValue("subtext", typeof(string));
                Date = (DateTime)info.GetValue("date", typeof(DateTime));
            }
            catch
            {
                Text = null;
                Subtext = null;
                Date = DateTime.Now;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("text", Text);
            info.AddValue("subtext", Subtext);
            info.AddValue("date", Date);
        }

        #endregion
    }
}
