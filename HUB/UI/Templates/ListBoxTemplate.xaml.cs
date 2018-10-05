namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections;

    /// <summary>
    /// Interaction logic for ListBoxTemplate.xaml
    /// </summary>
    public partial class ListBoxTemplate : UserControl
    {
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",
            typeof(IEnumerable), typeof(ListBoxTemplate), new FrameworkPropertyMetadata(null));

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemWidthProperty = DependencyProperty.Register("ListBoxItemWidth",
            typeof(int), (typeof(ListBoxTemplate)), new FrameworkPropertyMetadata(null));

        public int ListBoxItemWidth
        {
            get { return (int)GetValue(ListBoxItemWidthProperty); }
            set { SetValue(ListBoxItemWidthProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemHeightProperty = DependencyProperty.Register("ListBoxItemHeight",
            typeof(int), (typeof(ListBoxTemplate)), new FrameworkPropertyMetadata(null));

        public int ListBoxItemHeight
        {
            get { return (int)GetValue(ListBoxItemHeightProperty); }
            set { SetValue(ListBoxItemHeightProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemImageHeightProperty = DependencyProperty.Register("ListBoxItemImageHeight",
            typeof(int), (typeof(ListBoxTemplate)), new FrameworkPropertyMetadata(null));

        public int ListBoxItemImageHeight
        {
            get { return (int)GetValue(ListBoxItemImageHeightProperty); }
            set { SetValue(ListBoxItemImageHeightProperty, value); }
        }

        public ListBoxTemplate()
        {
            InitializeComponent();
        }
    }
}
