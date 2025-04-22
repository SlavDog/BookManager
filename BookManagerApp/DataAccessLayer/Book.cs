using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagerApp.DataAccessLayer
{
    public class Book
    {
        public Book() { }
        public Book(string author, string title, string bookshelf, User owner, int? myRating = null, DateTime? finishDate = null, string? genre = null)
        {
            Author = author;
            Title = title;
            MyRating = myRating;
            FinishDate = finishDate;
            Bookshelf = bookshelf;
            Genre = genre;
            User = owner;
        }
        [Required]
        public string Author { get; set; }
        [Key]
        public string Title { get; set; }
        public int? MyRating { get; set; }
        public DateTime? FinishDate { get; set; }
        [Required]
        public string Bookshelf { get; set; }
        public string? Genre { get; set; }
        [ForeignKey("User")]
        public string Username { get; set; }
        [Required]
        public User User { get; set; } 
    }
}
