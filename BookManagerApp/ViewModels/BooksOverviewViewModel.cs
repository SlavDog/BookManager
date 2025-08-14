using System.Collections.ObjectModel;
using System.Windows;
using BookManagerApp.DataAccessLayer;
using BookManagerApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using BookManagerApp.Managers;
using Microsoft.Win32;
using System.IO;
using BookManagerApp.Csv;
using System.Diagnostics;

namespace BookManagerApp.ViewModels
{
    public partial class BooksOverviewViewModel : ObservableObject
    {
        public User User { get; set; }
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesCommand))]
        private bool dataGridHasChanged = false;

        [ObservableProperty]
        private string filterString = "";

        [ObservableProperty]
        private string importResult = "";
        
        [ObservableProperty]
        private string exportResult = "";

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
            Debug.Assert(User != null && User.Username != null);
            User = await UserManager.GetUser(User.Username);
            LoadBooks(User.Books);
        }

        [RelayCommand]
        private void OpenAddBook(object obj)
        {
            var addBookWindow = new AddBook(User, this);
            var booksWindow = obj as BooksOverview;
            addBookWindow.Owner = booksWindow;
            Debug.Assert(booksWindow != null);
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addBookWindow.Show();
        }

        [RelayCommand]
        private void OpenHelp(object obj)
        {
            var helpWindow = new Help();
            var booksWindow = obj as BooksOverview;
            helpWindow.Owner = booksWindow;
            Debug.Assert(booksWindow != null);
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            helpWindow.Show();
        }

        [RelayCommand]
        private void OpenGraph(object obj)
        {
            var graphWindow = new Graph(Books);
            var booksWindow = obj as BooksOverview;
            graphWindow.Owner = booksWindow;
            Debug.Assert(booksWindow != null);
            booksWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            graphWindow.Show();
        }

        [RelayCommand]
        private void LogOut(object obj)
        {
            var loginWindow = new Login();
            var booksWindow = obj as BooksOverview;
            Debug.Assert(booksWindow != null);
            loginWindow.Show();
            booksWindow.Close();
        }

        [RelayCommand]
        private async Task DeleteBook(Book book)
        {
            await BookManager.DeleteBook(book);
            await ReloadUser();
        }

        [RelayCommand]
        private async Task Import(string replaceBooks)
        {
            OpenFileDialog fileDialog = new();
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files (*.csv)|*.csv";
            fileDialog.Title = "Load your books";
            string downloadsFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads"
            );
            if (Directory.Exists(downloadsFolder))
            {
                fileDialog.InitialDirectory = downloadsFolder;
            }
            if (fileDialog.ShowDialog() == true)
            {
                string selectedFile = fileDialog.FileName;
                try
                {
                    await CsvManager.ImportBooksFromCSV(selectedFile, User);
                    LoadBooks(User.Books);
                    ImportResult = $"Successful import!";
                }
                catch
                {
                    ImportResult = $"Something went wrong.";
                }
            }
        }

        [RelayCommand]
        private async Task Export(string replaceBooks)
        {
            SaveFileDialog fileDialog = new();
            fileDialog.FileName = "books.csv";
            fileDialog.DefaultExt = ".csv";
            fileDialog.Filter = "CSV files (*.csv)|*.csv";
            fileDialog.Title = "Save your books";
            if (fileDialog.ShowDialog() == true)
            {
                string selectedFile = fileDialog.FileName;
                try
                {
                    await CsvManager.ExportBooksToCSV(selectedFile, User);
                    ExportResult = $"Successful export!";

                }
                catch
                {
                    ExportResult = $"Something went wrong.";
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanSaveChanges))]
        private async Task SaveChanges()
        {
            await BookManager.UpdateBooks(Books);
            DataGridHasChanged = false;
        }

        public bool CanSaveChanges()
        {
            return DataGridHasChanged;
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
