namespace Portfolio.Areas.News.Models.ViewModel.Home
{
    public class RecentlyViewModel
    {
        public IEnumerable<DisplayArticleModel> ArticleList { get; set; } = null!;
        public DisplayArticleModel Spotlight { get; set; } = null!;
    }
}
