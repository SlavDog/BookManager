using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using BookManagerApp.DataAccessLayer;
using Microsoft.EntityFrameworkCore;

namespace BookManagerApp.Managers
{
    public class BookManager
    {
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
                    .Where(b => (b.Title ?? "").Contains(value));
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
                .Where(b => (b.Title ?? "").Contains(titleFilter) && (b.Author ?? "").Contains(authorFilter)
                            && (ratingCorrect ? (b.MyRating == rating) : ratingFilter == "")
                            && (finishCorrect ? (b.FinishDate == date) : finishFilter == "")
                            && ((b.Bookshelf ?? "").Contains(bookshelfFilter))
                            && (b.Genre == null ? false : b.Genre.Contains(genreFilter)));
            return newBooks;
        }
    }
}
