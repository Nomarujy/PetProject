using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel.Analytics;
using Portfolio.Areas.News.Models.ViewModel.Home;

namespace Portfolio.Areas.News.Services.Repository
{
    public interface IArticleRepository
    {
        #region Article
        Task AddArticleAsync(Article model);
        Task<Article?> GetAsync(int Id);
        Task UpdateAsync(Article form);

        #endregion Article

        Task<RecentlyViewModel> GetRecentlyAsync(int count = 5);

        Task<IEnumerable<DisplayArticleModel>> GetAuthorArticlesAsync(string authorId, int Page = 0, int count = 10);

        #region History

        Task AddToUserHistoryAsync(string userId, int articleId);
        Task<IEnumerable<ArticleViewers>> GetUserHistoryAsync(string userId, int Page = 0, int count = 10);

        #endregion History

        Task<AnalyticModel> GetArticleAnaliticsAsync(int articleId);
    }
}