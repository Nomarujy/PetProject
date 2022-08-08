using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Post;
using Portfolio.Data.Context;

namespace Portfolio.Areas.News.Data.Post.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContext database;

        public PostRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void Add(PostModel post)
        {
            database.Posts.Add(post);
            database.SaveChanges();
        }

        public PostModel? FindFirstPost(int Id)
        {
            return database.Posts.FirstOrDefault(p => p.Id == Id);
        }

        public PostModel? GetPostWithAuthor(int Id)
        {
            return database.Posts.Include(p => p.Author).FirstOrDefault(p => p.Id == Id);
        }

        public IEnumerable<PostModel> RecentlyPosts(int count = 5)
        {
            return database.Posts.OrderByDescending(c => c).Where(c=> c.IsDeleted == false && c.IsPubleched == true).Include(c => c.Author).Take(count).ToArray();
        }

        public void Update(PostModel post)
        {
            database.Posts.Update(post);
            database.SaveChanges();
        }
    }
}
