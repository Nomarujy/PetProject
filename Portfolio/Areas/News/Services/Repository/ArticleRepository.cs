using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel;
using Portfolio.Models;

namespace Portfolio.Areas.News.Services.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _database;

        public ArticleRepository(ApplicationDbContext database)
        {
            _database = database;
        }

        public async Task<Article?> FindByIdAsync(int Id)
        {
            return await _database.Articles.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<IEnumerable<Article>> GetRecentlyAsync(int count)
        {
            return await _database.Articles
                .Where(a => a.IsDeleted == false && a.IsPubleshed)
                .OrderByDescending(a => a.Id)
                .Take(count).ToArrayAsync();
        }

        public async Task AddArticleAsync(Article model)
        {
            _database.Articles.Add(model);
            await _database.SaveChangesAsync();
        }

        public async Task UpdateArticleAsync(Article article)
        {
            _database.Articles.Update(article);
            await _database.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> GetArticlesByAuthorIdAsync(string authorId)
        {
            return await _database.Articles
                .Where(a => a.AuthorId == authorId)
                .ToArrayAsync();
        }


        public async Task WriteToHistory(string userId, int articleId)
        {
            ArticleViewers model = new()
            {
                ViewerId = userId,
                ArticleId = articleId,
                ViewTime = DateTime.UtcNow,
            };

            _database.Add(model);
            await _database.SaveChangesAsync();
        }
        public async Task<IEnumerable<ArticleViewers>> GetUserHistory(string userId)
        {
            return await _database.ArticleViewers
                .Include(av => av.Article)
                .Where(av => av.ViewerId == userId)
                .OrderByDescending(av => av.Id)
                .Take(10).ToArrayAsync();
        }

        public async Task<AnalyticModel?> GetAnaliticsByIdAsync(int articleId, string AuthorId)
        {
            var res = await _database.Articles.Select(c => new {
                c.AuthorId,
                Views = _database.ArticleViewers.Where(av=> av.Id == articleId).Count(),
                UnicalViews = _database.ArticleViewers.Where(av => av.Id == articleId).GroupBy(av=> av.Id).Count(),
                LastHour = _database.ArticleViewers.Where(av => av.ArticleId == articleId).Count(av => av.ViewTime.Hour > DateTime.UtcNow.Hour - 1),
                Today = _database.ArticleViewers.Where(av => av.ArticleId == articleId).Count(av => av.ViewTime.Day == DateTime.UtcNow.Day)
            }).FirstOrDefaultAsync();

            if (res == null || res.AuthorId != AuthorId)
            {
                return null;
            }

            AnalyticModel model = new()
            {
                Views = res.Views,
                UnicalViews = res.UnicalViews,
                LastHour = res.LastHour,
                Today = res.Today
            };
            return model;
        }
    }
}
