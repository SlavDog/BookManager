using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookManagerApp.DataAccessLayer;
using BookManagerApp.Views;
using CommunityToolkit.Mvvm.Input;

namespace BookManagerApp.ViewModels
{
    partial class BooksOverviewViewModel(User user)
    {
        public User User { get; set; } = user;

        [RelayCommand]
        public void OpenAddBook(object obj)
        {
            var addBookWindow = new AddBook(User);
            var booksWindow = obj as BooksOverview;
            addBookWindow.Owner = booksWindow;
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addBookWindow.Show(); ;
        }
    }
}
