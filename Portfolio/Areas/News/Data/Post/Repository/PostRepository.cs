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
            try
            {
                database.Posts.Add(post);
                database.SaveChanges();
            }
            catch (Exception)
            {

            }
        }

        public PostModel? FindFirstPost(int Id)
        {
            try
            {
                return database.Posts.FirstOrDefault(p => p.Id == Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public PostModel? GetPostWithAuthor(int Id)
        {
            try
            {
                return database.Posts.Include(p => p.Author).FirstOrDefault(p => p.Id == Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<PostModel> GetRecentlyPosts(int count = 5)
        {
            try
            {
                return database.Posts.OrderByDescending(c => c).Where(c => c.IsDeleted == false && c.IsPubleched == true).Include(c => c.Author).Take(count).ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<PostModel>();
            }
        }

        public void Update(PostModel post)
        {
            try
            {
                database.Posts.Update(post);
                database.SaveChanges();
            }
            catch (Exception)
            {
            }
        }
    }
}
