using BookManagerApp.Managers;
using Microsoft.EntityFrameworkCore;

namespace BookManagerApp.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;
                    Initial Catalog=UserDB; Integrated Security=true";

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var salt = UserManager.GenerateSalt();
            modelBuilder.Entity<User>().HasData
                (
                    new User {
                        Username = "karel",
                        PasswordHash = UserManager.HashPassword("heslo", salt),
                        Salt = salt
                    }
                );
            modelBuilder.Entity<Book>().HasData
                (
                    new Book { Author = "Karel Čapek", Title = "Válka s mloky", MyRating = 5, FinishDate = new DateTime(2020, 5, 4), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -1},
                    new Book { Author = "Karel Čapek", Title = "R.U.R.", MyRating = 5, FinishDate = new DateTime(2021, 9, 7), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -2 },
                    new Book { Author = "Karel Čapek", Title = "Krakatit", MyRating = 5, FinishDate = new DateTime(2021, 7, 7), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -3 },
                    new Book { Author = "William Golding", Title = "Pán Much", MyRating = 5, FinishDate = new DateTime(2022, 12, 7), Bookshelf = "Completed", Genre = "Dystopian", Username = "karel", Id = -4 },
                    new Book { Author = "George Orwell", Title = "1984", MyRating = 5, FinishDate = new DateTime(2020, 11, 6), Bookshelf = "Completed", Genre = "Dystopian", Username = "karel", Id = -5 },
                    new Book { Author = "George Orwell", Title = "Farma zvířat", MyRating = 5, FinishDate = new DateTime(2022, 10, 6), Bookshelf = "Completed", Genre = "Dystopian", Username = "karel", Id = -6 },
                    new Book { Author = "Haruki Murakami", Title = "Kafka na pobřeží", MyRating = 5, FinishDate = new DateTime(2022, 10, 21), Bookshelf = "Completed", Genre = "Unknown", Username = "karel", Id = -7 },
                    new Book { Title = "Bohatý táta, chudý táta", Author = "Robert Kiyosaki", MyRating = 5, FinishDate = new DateTime(2021, 3, 21), Bookshelf = "Completed", Genre = "Self-Help", Username = "karel", Id = -8 },
                    new Book { Title = "Nejbohatší muž v Babyloně", Author = "George Samuel Clason", MyRating = 5, FinishDate = new DateTime(2021, 4, 20), Bookshelf = "Completed", Genre = "Self-Help", Username = "karel", Id = -9 },
                    new Book { Title = "Růže pro Algernon", Author = "Daniel Keyes", MyRating = 5, FinishDate = new DateTime(2021, 4, 20), Bookshelf = "Completed", Genre = "Unknown", Username = "karel", Id = -10 },
                    new Book { Title = "Duna", Author = "Frank Herbert", MyRating = 5, FinishDate = new DateTime(2022, 7, 20), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -11 },
                    new Book { Title = "Problém tří těles", Author = "Liou Cch'-Sin", MyRating = 5, FinishDate = new DateTime(2023, 7, 20), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -12 },
                    new Book { Title = "Temný les", Author = "Liou Cch'-Sin", MyRating = 5, FinishDate = new DateTime(2023, 8, 18), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -13 },
                    new Book { Title = "Vzpomínka na zemi", Author = "Liou Cch'-Sin", MyRating = 5, FinishDate = new DateTime(2023, 9, 18), Bookshelf = "Completed", Genre = "Sci-Fi", Username = "karel", Id = -14 },
                    new Book { Title = "Umění války", Author = "Sun-c'", MyRating = 5, FinishDate = new DateTime(2023, 10, 18), Bookshelf = "Completed", Genre = "Self-Help", Username = "karel", Id = -15 },
                    new Book { Title = "Balady a romance", Author = "Jan Neruda", MyRating = 5, FinishDate = new DateTime(2023, 11, 18), Bookshelf = "Completed", Genre = "Poetry", Username = "karel", Id = -16 },
                    new Book { Title = "České nebe", Author = "Jára Cimrman", MyRating = 5, FinishDate = new DateTime(2023, 12, 17), Bookshelf = "Completed", Genre = "Classics", Username = "karel", Id = -17 },
                    new Book { Title = "Lakomec", Author = "Moliére", MyRating = 5, FinishDate = new DateTime(2022, 11, 14), Bookshelf = "Completed", Genre = "Classics", Username = "karel", Id = -18 }
                );
        }
    }
}
