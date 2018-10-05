namespace HUB.Views.DialogViews
{
    using HUB.ViewModels.DialogViewModels;
    using System;
    using System.Windows;
    /// <summary>
    /// Interaction logic for WebsiteDetailsAlternateDialogView.xaml
    /// </summary>
    public partial class WebsiteDetailsAlternateDialogView : Window
    {
        public WebsiteDetailsAlternateDialogView()
        {
            InitializeComponent();
            DataContext = new WebsiteDetailsViewModel();
            var desc = "Game of Thrones is an American fantasy drama television series created by David Benioff and D. B. Weiss. It is an adaptation of A Song of Ice and Fire, George R. R. Martin's series of fantasy novels, the first of which is A Game of Thrones. It is filmed in Belfast and elsewhere in Northern Ireland, Canada, Croatia, Iceland, Malta, Morocco, Spain, and the United States. The series premiered on HBO in the United States on April 17, 2011, and its seventh season ended on August 27, 2017. The series will conclude with its eighth season premiering in 2019.";
            var link = "https://www.seriestop.net/show/game-of-thrones";
            ((WebsiteDetailsViewModel)DataContext).Website = new Models.TVShow("Game of thrones", desc,
                link, "C:\\Users\\Rokas\\Documents\\Personal\\C#\\HUB\\HUB\\bin\\Debug\\thumbnails\\2c5eef26-ed2c-4a54-904d-652c49e7f45a.png", null, null, null, 1, 10, 2, 4, DateTime.Parse("2018-5"));
        }
    }
}
