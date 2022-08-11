using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Services.Repository
{
    public interface IArticleRepository
    {
        Task AddArticleAsync(Article model);
        Task<Article?> FindByIdAsync(int Id);
        Task<IEnumerable<Article>> GetRecentlyAsync(int count);
        Task UpdateArticleAsync(Article article);
    }
}