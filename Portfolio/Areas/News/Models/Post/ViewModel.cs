namespace Portfolio.Areas.News.Models.Post
{
    public class ViewModel
    {
        public PostModel CurrentPost { get; set; } = null!;

        public IEnumerable<PostModel> RecentlyPosts { get; set; } = null!;
        public PostModel Spotlight { get; set; } = null!;
    }
}
