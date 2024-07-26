using Microsoft.EntityFrameworkCore;

namespace Library.API.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Entities.Book> Books { get; set; }
        public DbSet<Entities.Author> Authors { get; set; }
        public DbSet<Entities.Publisher> Publishers { get; set; }
        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Entities.Genre> Genres { get; set; }

           
    }
}
