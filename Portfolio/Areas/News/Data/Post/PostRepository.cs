using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Post;
using Portfolio.Data.Context;
namespace Portfolio.Areas.News.Data.Post
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext database;

        public PostRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void AddPost(PostModel post)
        {
            database.Posts.Add(post);
            database.SaveChanges();
        }

        public PostModel? GetPostWithAuthor(int Id)
        {
            return database.Posts.Include(p => p.Author).FirstOrDefault(p => p.Id == Id);
        }
    }
}
