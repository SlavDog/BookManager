using BookManagerApp.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using BookManagerApp.Views;
using BookManagerApp.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

namespace BookManagerApp.ViewModels
{
    public partial class AddBookViewModel(User user, BooksOverviewViewModel parentView) : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
        private string? title;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
        public string? author;

        [ObservableProperty]
        private DateTime? finishDate;

        [ObservableProperty]
        private int? rating = 0;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
        private string? bookshelf;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(AddBookCommand))]
        private string? genre = "Unknown";

        [ObservableProperty]
        private string showText = "";

        public string[] Genres { get; } = ComboBoxValues.Genres;

        public string[] Bookshelves { get;  } = ComboBoxValues.Bookshelves;

        [RelayCommand(CanExecute = nameof(CanAddBook))]
        public async Task AddBook()
        {
            Debug.Assert(Author != null && Title != null && Bookshelf != null && user != null);
            bool success = await BookManager.AddNewBook(new Book(Author, Title, Bookshelf, user, Rating, FinishDate, Genre));
            if (success)
            {
                await parentView.ReloadUser();
                Title = "";
                Author = "";
                FinishDate = null;
                Rating = 0;
                Genre = "Unknown";
                Bookshelf = null;
                ShowText = "";
            }
            else
            {
                ShowText = "A book with this name already exists!";
            }
        }

        public bool CanAddBook()
        {
            return (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Author)
                && !string.IsNullOrEmpty(Bookshelf));
        }
    }
}
