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

namespace BookManagerApp.ViewModels
{
    partial class AddBookViewModel(User user)
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime FinishDate { get; set; }
        public float Rating { get; set; }
        public string Bookshelf { get; set; }
        public string Genre { get; set; }
        public User Owner { get; set; } = user;

        [RelayCommand]
        public void AddNewBook()
        {
            // TODO
        }
    }
}
