using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookManagerApp.DataAccessLayer
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Title { get; set; }
        public int? MyRating { get; set; }
        public DateTime? DateRead { get; set; }
        [Required]
        public string Bookshelf { get; set; }
        public string? Genre { get; set; }
    }
}
