using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.Csv
{
    public class BookExportDTO
    {
        // This class is used to represent the data structure of a book for export to CSV.
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int? MyRating { get; set; }
        public DateTime? FinishDate { get; set; }
        public string? Bookshelf { get; set; }
        public string? Genre { get; set; }
    }
}
