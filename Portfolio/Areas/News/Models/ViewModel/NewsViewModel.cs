using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Models.ViewModel
{
    public class NewsViewModel
    {
        public NewsViewModel(IEnumerable<Article> recentlyPosts, Article? spotlight, Article? article)
        {
            ArticleList = recentlyPosts;
            Spotlight = spotlight ?? new()
            {
                Title = "No Spotlight"
            };
            Article = article ?? new();
        }

        public IEnumerable<Article> ArticleList { get; set; }
        public Article Spotlight { get; set; }

        public Article Article { get; set; }
    }
}
