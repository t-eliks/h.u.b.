namespace HUB.UI.Templates
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ListBoxTemplate.xaml
    /// </summary>
    public partial class ListBoxItemTemplate : UserControl
    {
        public static readonly DependencyProperty ListBoxItemWidthProperty = DependencyProperty.Register("ListBoxItemWidth",
            typeof(int), (typeof(ListBoxItemTemplate)), new FrameworkPropertyMetadata(350));

        public int ListBoxItemWidth
        {
            get { return (int)GetValue(ListBoxItemWidthProperty); }
            set { SetValue(ListBoxItemWidthProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemHeightProperty = DependencyProperty.Register("ListBoxItemHeight",
            typeof(int), (typeof(ListBoxItemTemplate)), new FrameworkPropertyMetadata(280));

        public int ListBoxItemHeight
        {
            get { return (int)GetValue(ListBoxItemHeightProperty); }
            set { SetValue(ListBoxItemHeightProperty, value); }
        }

        public static readonly DependencyProperty ListBoxItemTitleImageHeightProperty = DependencyProperty.Register("ListBoxItemImageHeight",
            typeof(int), (typeof(ListBoxItemTemplate)), new FrameworkPropertyMetadata(230));

        public int ListBoxItemImageHeight
        {
            get { return (int)GetValue(ListBoxItemTitleImageHeightProperty); }
            set { SetValue(ListBoxItemTitleImageHeightProperty, value); }
        }

        public ListBoxItemTemplate()
        {
            InitializeComponent();
        }
    }
}
