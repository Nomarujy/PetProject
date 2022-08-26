using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Models.Authentication.Entity;

namespace Portfolio.Areas.News.Models.Entity
{
    public class ArticleViewversConfiguration : IEntityTypeConfiguration<ArticleViewers>
    {
        public void Configure(EntityTypeBuilder<ArticleViewers> builder)
        {
            builder.HasKey(av => av.Id);
            builder.HasIndex(av => av.ArticleId);
            builder.HasIndex(av => av.ViewerId);
        }
    }

    public class ArticleViewers
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;

        public string ViewerId { get; set; } = null!;
        public User Viewer { get; set; } = null!;

        public DateTime ViewTime { get; set; }
    }
}
