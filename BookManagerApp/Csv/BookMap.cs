using CsvHelper.Configuration;

namespace BookManagerApp.Csv
{
    public class BookMap : ClassMap<BookExportDTO>
    {
        public BookMap()
        {
            Map(b => b.Title).Name("Title");
            Map(b => b.Author).Name("Author");
            Map(b => b.MyRating).Name("My Rating");
            Map(b => b.FinishDate).Name("Date Read");
            Map(b => b.Bookshelf).Name("Exclusive Shelf");
            Map(b => b.Genre).Name("Genre").Optional();
        }
    }
}