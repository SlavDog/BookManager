using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using BookManagerApp.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BookManagerApp.Models
{
    public class BookManager
    {
        static int counter = 0;
        public async static Task<bool> AddNewBook(Book book, User user)
        {
            using var context = new AppDbContext();
            var temp = await context.Books
                .FirstOrDefaultAsync(b => b.Title == book.Title 
                                          && b.Username == user.Username
                                          && b.Author == book.Author);
            if (temp != null)
            {
                return false;
            }
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return true;
        }

        public async static Task DeleteBook(Book book)
        {
            using var context = new AppDbContext();
            var existing = await context.Books
                .FirstOrDefaultAsync(b => b.Id == book.Id);
            if (existing != null)
            {
                context.Entry(existing).State = EntityState.Detached;
                context.Books.Remove(book);
            }
            await context.SaveChangesAsync();
        }

        public async static Task UpdateBooks(ObservableCollection<Book> books)
        {
            using var context = new AppDbContext();

            foreach (var book in books)
            {
                context.Books.Update(book);
            }

            await context.SaveChangesAsync();
        }

        public static IEnumerable<Book> GetFilteredBooks(User user, string value)
        {
            IEnumerable<Book> newBooks;

            Match titleMatch = Regex.Match(value, @"(?i)(?:Title=""([^""]+)"")");
            Match authorMatch = Regex.Match(value, @"(?i)(?:Author=""([^""]+)"")");
            Match ratingMatch = Regex.Match(value, @"(?i)(?:Rating=""([^""]+)"")");
            Match finishMatch = Regex.Match(value, @"(?i)(?:Finish=""([^""]+)"")");
            Match bookshelfMatch = Regex.Match(value, @"(?i)(?:Bookshelf=""([^""]+)"")");
            Match genreMatch = Regex.Match(value, @"(?i)(?:Genre=""([^""]+)"")");

            if ((!titleMatch.Success && !authorMatch.Success && !ratingMatch.Success
                && !finishMatch.Success && !bookshelfMatch.Success && !genreMatch.Success))
            {
                newBooks = (user.Books ?? Enumerable.Empty<Book>())
                    .Where(b => b.Title.Contains(value));
                return newBooks;
            }

            string titleFilter = titleMatch.Success ? titleMatch.Groups[1].Value : "";
            string authorFilter = authorMatch.Success ? authorMatch.Groups[1].Value : "";
            string ratingFilter = ratingMatch.Success ? ratingMatch.Groups[1].Value : "";
            string finishFilter = finishMatch.Success ? finishMatch.Groups[1].Value : "";
            string bookshelfFilter = bookshelfMatch.Success ? bookshelfMatch.Groups[1].Value : "";
            string genreFilter = genreMatch.Success ? genreMatch.Groups[1].Value : "";

            int rating;
            bool ratingCorrect = int.TryParse(ratingFilter, out rating);

            DateTime date;
            bool finishCorrect = DateTime.TryParse(finishFilter, out date);

            newBooks = (user.Books ?? Enumerable.Empty<Book>())
                .Where(b => b.Title.Contains(titleFilter) && b.Author.Contains(authorFilter)
                            && (ratingCorrect ? (b.MyRating == rating) : ratingFilter == "")
                            && (finishCorrect ? (b.FinishDate == date) : finishFilter == "")
                            && (b.Bookshelf.Contains(bookshelfFilter))
                            && (b.Genre == null ? false : b.Genre.Contains(genreFilter)));
            return newBooks;
        }

        public async static Task ImportBooksFromCSV(string filePath, User user)
        {
            using var context = new AppDbContext();
            using (var sourceStream =
            new FileStream(
                filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                using (var reader = new StreamReader(sourceStream))
                {
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        Quote = '"'
                    }))
                    {
                        await foreach (var record in csv.GetRecordsAsync<dynamic>())
                        {
                            var dict = (IDictionary<string, object>)record;

                            int rating;
                            Int32.TryParse(dict["My Rating"]?.ToString(), out rating);

                            DateTime dateTime;
                            DateTime.TryParse(dict["Date Read"]?.ToString(), out dateTime);

                            if (await context.Books
                                .FirstOrDefaultAsync(b => b.Title == dict["Title"].ToString() 
                                                          && b.Username == user.Username
                                                          && b.Author == dict["Author"].ToString()) != null) 
                            {
                                continue;
                            }
                            var book = new Book(dict["Author"].ToString() ?? $"Author{counter}",
                                                    dict["Title"].ToString() ?? $"Book{counter++}",
                                                    convertGoodReadsBookshelf(dict["Exclusive Shelf"]?.ToString() ?? "to-read"),
                                                    user.Username,
                                                    dict["My Rating"].ToString() == "" ? null : rating,
                                                    dict["Date Read"].ToString() == "" ? null : dateTime,
                                                    "Unknown");
                            user.Books.Add(book);
                            context.Add(book);
                        }
                    }
                }
                await context.SaveChangesAsync();
            }
        }

        public static async Task ExportBooksToCSV(string filePath, User user)
        {
            using var context = new AppDbContext();
            using (var sourceStream =
            new FileStream(
                filePath,
                FileMode.Create, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                using (var writer = new StreamWriter(sourceStream))
                {
                    using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Delimiter = ",",
                        Quote = '"'
                    }))
                    {
                        var infoToExport = user.Books
                            .Select(b => new {
                                b.Title,
                                b.Author,
                                b.MyRating,
                                b.FinishDate,
                                b.Bookshelf,
                                b.Genre
                            });
                        await csv.WriteRecordsAsync(infoToExport);
                    }
                }
            }
        }

        private static Func<string, string> convertGoodReadsBookshelf = input => input switch
        {
            "read" => "Completed",
            "currently-reading" => "Currently Reading",
            "to-read" => "Want To Read",
            _ => "Want To Read"
        };
    }
}
