using Microsoft.EntityFrameworkCore;

namespace MessageConsumer
{
    public class MessageRepository : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);

            string connection = "Host=192.168.1.200:35432;Username=postgres;Password=postgres;Database=Dev";

            optionsBuilder.UseNpgsql(connection, opt => opt.EnableRetryOnFailure());
        }

        public DbSet<UserMessage> Chat { get; set; } = null!;
    }
}