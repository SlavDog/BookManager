using BookManagerApp.DataAccessLayer;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BookManagerApp.Csv
{
    public static class CsvManager
    {
        static int counter = 0;
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
                        Quote = '"',
                        MissingFieldFound = null

                    }))
                    {
                        csv.Context.RegisterClassMap<BookMap>();
                        await foreach (var record in csv.GetRecordsAsync<BookExportDTO>())
                        {
                            if (await context.Books
                                .FirstOrDefaultAsync(b => b.Title == record.Title
                                                          && b.Username == user.Username
                                                          && b.Author ==record.Author) != null)
                            {
                                continue;
                            }
                            Debug.Assert(user.Username != null);
                            var book = new Book(record.Author ?? $"Author{counter}",
                                                    record.Title ?? $"Book{counter++}",
                                                    convertGoodReadsBookshelf(record.Bookshelf ?? "to-read"),
                                                    user.Username,
                                                    record.MyRating,
                                                    record.FinishDate,
                                                    record.Genre ?? "Unknown");
                            Debug.Assert(user.Books != null);
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
                        Debug.Assert(user.Books != null);
                        var infoToExport = user.Books
                            .Select(b => new BookExportDTO {
                                Title = b.Title,
                                Author = b.Author,
                                MyRating = b.MyRating,
                                FinishDate = b.FinishDate,
                                Bookshelf = b.Bookshelf,
                                Genre = b.Genre
                            });

                        csv.Context.RegisterClassMap<BookMap>();
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
