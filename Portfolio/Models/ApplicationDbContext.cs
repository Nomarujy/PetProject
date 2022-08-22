using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Models.Authentication.Entity;

namespace Portfolio.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        protected ApplicationDbContext() : base() { Database.Migrate(); }

        public ApplicationDbContext(DbContextOptions opt) : base(opt) { Database.Migrate(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region News
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ArticleViewers> ArticleViewers { get; set; } = null!;


        #endregion News
    }
}
