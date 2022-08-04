using Portfolio.Areas.News.Models.Post;

namespace Portfolio.Areas.News.Data.Post
{
    public interface IPostRepository
    {
        public void AddPost(PostModel post);
        public PostModel? GetPostWithAuthor(int Id);
    }
}