using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManagerApp.DataAccessLayer
{
    public class Book
    {
        public Book() { }
        public Book(string author, string title, string bookshelf, string owner, int? myRating = null, DateTime? finishDate = null, string? genre = null)
        {
            Author = author;
            Title = title;
            MyRating = myRating;
            FinishDate = finishDate;
            Bookshelf = bookshelf;
            Genre = genre;
            Username = owner;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Author { get; set; }
        public string? Title { get; set; }
        public int? MyRating { get; set; }
        public DateTime? FinishDate { get; set; }
        [Required]
        public string? Bookshelf { get; set; }
        public string? Genre { get; set; }
        [ForeignKey(nameof(User))]
        public string? Username { get; set; }
        [Required]
        public User? User { get; set; } 
    }
}
