using BookManagerApp.DataAccessLayer;
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

namespace BookManagerApp.Views
{
    /// <summary>
    /// Interakční logika pro AddBook.xaml
    /// </summary>
    public partial class AddBook : Window
    {
        public AddBook(User user)
        {
            InitializeComponent();
            var addBookViewModel = new AddBookViewModel(user);
            DataContext = addBookViewModel;
        }
    }
}
