using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.Models.Authorization;

namespace Portfolio.Data.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { Database.Migrate(); }

        public DatabaseContext(DbContextOptions opt) : base(opt) { Database.Migrate(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(Role.GetDefaultRole());
        }

        public DbSet<Contact> Contact { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
    }
}
