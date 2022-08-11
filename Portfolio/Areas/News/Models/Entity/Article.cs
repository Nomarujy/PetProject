using Portfolio.Models.Authentication.Entity;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Areas.News.Models.Entity
{
    public class ArticleConfigurator : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(p => p.Id).HasName("PK_Id");
            builder.HasOne(p => p.Author).WithMany()
                .HasForeignKey(p => p.AuthorId)
                .HasPrincipalKey(u=> u.Id)
                .HasConstraintName("FK_AuthorId");
        }
    }
    [EntityTypeConfiguration(typeof(ArticleConfigurator))]
    public class Article
    {
        public int Id { get; set; }

        [MaxLength(64)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string AuthorUserName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;
        public User Author { get; set; } = null!;

        public DateTime DateOfPublishing { get; set; }
        public bool IsPubleshed { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
