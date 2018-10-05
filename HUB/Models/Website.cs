namespace HUB.Models
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class Website : Item
    {
        #region Constructors

        public Website()
        {
            Title = null;
            ImageLocation = null;
            EdgeColor = null;
            MiddleColor = null;
            HighlightColor = null;
            Description = null;
            Link = null;
        }

        public Website(string title, string imagelocation, string edgecolor, string middlecolor, string highlightcolor, string description, string link)
            : base(title, imagelocation, edgecolor, middlecolor, highlightcolor)
        {
            Title = title;
            ImageLocation = imagelocation;
            EdgeColor = edgecolor;
            MiddleColor = middlecolor;
            HighlightColor = highlightcolor;
            Description = description;
            Link = link;
        }

        #endregion

        #region Fields and Properties

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

                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

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

                    RaisePropertyChanged(nameof(Link));
                }
            }
        }

        #endregion

        #region Serialization

        protected Website(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            try
            {
                
                Description = info.GetString("description");
                Link = info.GetString("link");
            }
            catch
            {
                Description = null;
                Link = null;
            }
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("description", Description);
            info.AddValue("link", Link);
        }

        #endregion
    }
}
