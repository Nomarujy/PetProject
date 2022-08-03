using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
using Portfolio.Models.Authorization;

namespace Portfolio.Data.Database.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base() { }

        public DatabaseContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                Role.GetDefaultUser(), 
                Role.GetDefaultAdmin());
        }

        public DbSet<Contact> Contact { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permision> Permisions { get; set; } = null!;
    }
}
