using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Models.Authentication.Entity;
using Portfolio.Models.StartPage;

namespace Portfolio.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions opt) : base(opt) { Database.Migrate(); }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
            string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
            string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";
            string database = Environment.GetEnvironmentVariable("POSTGRES_DATABASE") ?? "postgres";

            string connectionString = $"Host={host};Username={username};Password={password};Database={database}";

            optionsBuilder.UseNpgsql(connectionString,
                opt => opt.EnableRetryOnFailure());
        }

        public DbSet<MessageModel> Messages { get; set; } = null!;


        #region News
        public DbSet<Article> Articles { get; set; } = null!;
        public DbSet<ArticleViewers> ArticleViewers { get; set; } = null!;


        #endregion News
    }
}
