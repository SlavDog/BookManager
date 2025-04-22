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
    }
}
