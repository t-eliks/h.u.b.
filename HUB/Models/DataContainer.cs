namespace HUB.Models
{
    using System;
    using System.Collections.ObjectModel;

    [Serializable]
    public class DataContainer
    {
        public ObservableCollection<VideoGame> Games { get; set; }

        public ObservableCollection<Website> Websites { get; set; }

        public ObservableCollection<Event> Events { get; set; }
    }
}
