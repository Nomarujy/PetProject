using Microsoft.EntityFrameworkCore;
using Portfolio.Models.Authorization;
using Portfolio.Models.Contact;

using Portfolio.Areas.News.Models.Post;

namespace Portfolio.Data.Context
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

        public DbSet<ContactModel> Contact { get; set; } = null!;

        #region Authorization
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Permision> Permisions { get; set; } = null!;

        #endregion Authorization

        #region News
        public DbSet<PostModel> Posts { get; set; } = null!;

        #endregion News
    }
}
