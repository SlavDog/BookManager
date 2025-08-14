using BookManagerApp.DataAccessLayer;
using BookManagerApp.ViewModels;
using System.Windows;

namespace BookManagerApp.Views
{
    /// <summary>
    /// Interakční logika pro AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook(User user, BooksOverviewViewModel parentView)
        {
            InitializeComponent();
            var addBookViewModel = new AddBookViewModel(user, parentView);
            DataContext = addBookViewModel;
        }
    }
}
