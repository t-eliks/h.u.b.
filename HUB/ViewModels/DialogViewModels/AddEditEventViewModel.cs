namespace HUB.ViewModels.DialogViewModels
{
    using HUB.Logic;
    using HUB.Logic.Dialog;
    using HUB.Models;
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class AddEditEventViewModel : BaseViewModel
    {
        public AddEditEventViewModel()
        {
            Date = DateTime.Today;

            AddEventCommand = new RelayCommand(AddEvent);
        }

        public event RoutedEventHandler OnAddedEvent;

        public ICommand AddEventCommand { get; private set; }

        public bool EditMode { get; set; }

        private Event _Event;

        public Event Event
        {
            get
            {
                return _Event;
            }
            set
            {
                if (_Event != value)
                {
                    _Event = value;

                    RaisePropertyChanged(nameof(Event));
                }
            }
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

                    if (EditMode)
                        Event.Text = value;

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

                    if (EditMode)
                        Event.Subtext = value;

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

                    if (EditMode)
                        Event.Date = value;

                    RaisePropertyChanged(nameof(Date));
                }
            }
        }

        private double? _Hour;

        public double? Hour
        {
            get
            {
                return _Hour;
            }
            set
            {
                if (_Hour != value)
                {
                    _Hour = value;

                    RaisePropertyChanged(nameof(Hour));
                }
            }
        }

        private double? _Minutes;

        public double? Minutes
        {
            get
            {
                return _Minutes;
            }
            set
            {
                if (_Minutes != value)
                {
                    _Minutes = value;

                    RaisePropertyChanged(nameof(Minutes));
                }
            }
        }

        public void AddEvent()
        {
            if (!EditMode)
            {
                Date = Date.AddHours(Hour != null ? (double)Hour : 0);

                Date = Date.AddMinutes(Minutes != null ? (double)(Minutes) : 0);

                Event evnt = new Event(Text, Subtext, Date);

                OnAddedEvent?.Invoke(this, new DialogRoutedEventArgs() { Object = evnt });
            }
            else
            {
                Event.Date = Event.Date.Date.AddHours((double)Hour);

                Event.Date = Event.Date.AddMinutes((double)Minutes);

                OnAddedEvent?.Invoke(this, new DialogRoutedEventArgs() { Object = null });
            }
        }

    }
}
