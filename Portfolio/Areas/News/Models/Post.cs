using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portfolio.Areas.News.Models
{
    public class PostConfigurator : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public DateTime Publeshed { get; set; } = DateTime.Now;
        public string Title { get; set; } = "";
        public string Content { get; set; } = "";

        public bool IsDeleted { get; set; } = false;
    }
}
