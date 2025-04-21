using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerApp.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        private string connectionString = @"server=(localdb)\MSSQLLocalDB;
                    Initial Catalog=UserDB; Integrated Security=true";

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
