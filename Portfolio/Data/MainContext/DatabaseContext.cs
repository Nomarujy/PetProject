using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Data.MainContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DatabaseContext(DbContextOptions opt) : base(opt) { }

        public DbSet<ContactWithMe> contactWithMe { get; set; } = null!;
    }
}
