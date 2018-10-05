namespace HUB.UI.Templates
{
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ContextMenuOptionsTemplate.xaml
    /// </summary>
    public partial class ContextMenuOptionsTemplate : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable), typeof(ContextMenuOptionsTemplate), new FrameworkPropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public ContextMenuOptionsTemplate()
        {
            InitializeComponent();
        }
    }
}
