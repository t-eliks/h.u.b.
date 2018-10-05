namespace HUB.ViewModels.DialogViewModels
{
    using HUB.Logic;
    using HUB.Logic.Dialog;
    using HUB.Models;
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Input;

    public class AddWebsiteDialogViewModel : AddEditObjectViewModelBase
    {
        public AddWebsiteDialogViewModel()
        {
            CloseCommand = new RelayCommand<string>(AddItem);
        }

        public ICommand CloseCommand { get; private set; }

        #region Fields and Properties

        public new event RoutedEventHandler OnItemAdded;

        private string _Link;

        public string Link
        {
            get
            {
                return _Link;
            }
            set
            {
                if (_Link != value)
                {
                    _Link = value;

                    if (EditMode && Item != null && Item is Website)
                        ((Website)Item).Link = value;

                    RaisePropertyChanged(nameof(Link));
                }
            }
        }

        private string _Description;

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                if (_Description != value)
                {
                    _Description = value;

                    if (EditMode && Item != null && Item is Website)
                        ((Website)Item).Description = value;

                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        private bool _Regular = true;

        public bool Regular
        {
            get
            {
                return _Regular;
            }
            set
            {
                if (_Regular != value)
                {
                    _Regular = value;

                    if (_Regular)
                        TVShow = false;

                    RaisePropertyChanged(nameof(Regular));
                    RaisePropertyChanged(nameof(TVShow));
                }
            }
        }

        private bool _TVShow;

        public bool TVShow
        {
            get
            {
                return _TVShow;
            }
            set
            {
                if (_TVShow != value)
                {
                    _TVShow = value;

                    if (_TVShow)
                        Regular = false;

                    RaisePropertyChanged(nameof(TVShow));
                    RaisePropertyChanged(nameof(Regular));
                }
            }
        }

        #endregion

        #region Methods

        public override void AddItem(string result)
        {
            Website website = null;

            if (!EditMode && bool.Parse(result))
            {
                if (Regular)
                    website = new Website(Title, Path.GetFileName(ImageLocation), EdgeColor, MiddleColor, HighlightColor, Description, Link);
                else if (TVShow)
                    website = new TVShow(Title,Path.GetFileName(ImageLocation), EdgeColor, MiddleColor, HighlightColor, Description, Link, 0, 0, 0, 0, DateTime.Now);

                website.ContextMenuMap = this.ContextMenuMap;

                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = website });
            }
            else
                OnItemAdded?.Invoke(this, new DialogRoutedEventArgs() { Object = null });

        }

        #endregion
    }
}
