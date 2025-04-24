using BookManagerApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookManagerApp.DataAccessLayer;
using BookManagerApp.Models;
using System.Globalization;

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
                vm.HasChanged = false;
                vm.HasChanged = true;
            }
        }
    }
}
