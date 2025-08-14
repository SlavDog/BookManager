using BookManagerApp.ViewModels;
using System.Windows;
using System.Windows.Controls;
using BookManagerApp.DataAccessLayer;

namespace BookManagerApp.Views
{
    /// <summary>
    /// Interakční logika pro BooksOverview.xaml
    /// </summary>
    public partial class BooksOverview : Window
    {
        public BooksOverview(User user)
        {
            InitializeComponent();
            var booksOverviewViewModel = new BooksOverviewViewModel(user);
            DataContext = booksOverviewViewModel;
        }

        private void BooksGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (DataContext is BooksOverviewViewModel vm && e.EditAction == DataGridEditAction.Commit)
            {
                vm.DataGridHasChanged = false;
                vm.DataGridHasChanged = true;
            }
        }
    }
}
