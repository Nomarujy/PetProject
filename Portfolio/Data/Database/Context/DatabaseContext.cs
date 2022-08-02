using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Data.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { Database.Migrate(); }

        public DatabaseContext(DbContextOptions opt) : base(opt) { Database.Migrate(); }


        public DbSet<Contact> Contact { get; set; } = null!;
    }
}
