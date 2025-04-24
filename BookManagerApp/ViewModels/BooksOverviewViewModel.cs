using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using BookManagerApp.DataAccessLayer;
using BookManagerApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookManagerApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BookManagerApp.ViewModels
{
    public partial class BooksOverviewViewModel : ObservableObject
    {
        public User User { get; set; }
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesCommand))]
        private bool hasChanged = false;

        [ObservableProperty]
        private string filterString = "";

        partial void OnFilterStringChanged(string value)
        {
            var newBooks = BookManager.GetFilteredBooks(User, value);
            LoadBooks(newBooks.ToArray());
        }

        public BooksOverviewViewModel(User user)
        {
            User = user;
            LoadBooks(User.Books);
        }


        public async Task ReloadUser()
        {
            User = await UserService.GetUser(User.Username);
            LoadBooks(User.Books);
        }

        [RelayCommand]
        private void OpenAddBook(object obj)
        {
            var addBookWindow = new AddBook(User, this);
            var booksWindow = obj as BooksOverview;
            addBookWindow.Owner = booksWindow;
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addBookWindow.Show();
        }

        [RelayCommand]
        private void OpenHelp(object obj)
        {
            var helpWindow = new Help();
            var booksWindow = obj as BooksOverview;
            helpWindow.Owner = booksWindow;
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            helpWindow.Show();
        }

        [RelayCommand]
        private void OpenGraph(object obj)
        {
            var graphWindow = new Graph(Books);
            var booksWindow = obj as BooksOverview;
            graphWindow.Owner = booksWindow;
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            graphWindow.Show();
        }

        [RelayCommand]
        private async Task DeleteBook(Book book)
        {
            await BookManager.DeleteBook(book);
            LoadBooks(User.Books);
        }

        [RelayCommand(CanExecute = nameof(CanSaveChanges))]
        private async Task SaveChanges()
        {
            await BookManager.UpdateBooks(Books);
            HasChanged = false;
        }

        public bool CanSaveChanges()
        {
            return HasChanged;
        }

        public void LoadBooks(ICollection<Book>? books)
        {
            Books.Clear();
            foreach (var book in books ?? Enumerable.Empty<Book>())
            {
                Books.Add(book);
            }
        }
    }
}
