using Portfolio.Areas.News.Models.Post;

namespace Portfolio.Areas.News.Data.Post.Repository
{
    public interface IPostRepository
    {
        public void Add(PostModel post);
        public PostModel? GetPostWithAuthor(int Id);
        public PostModel? FindFirstPost(int Id);

        public IEnumerable<PostModel> RecentlyPosts(int count = 5);

        public void Update(PostModel post);
    }
}