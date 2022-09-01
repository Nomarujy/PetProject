using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel.Analytics;
using Portfolio.Areas.News.Models.ViewModel.Home;
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

        #region BaseArticleAction

        public async Task AddArticleAsync(Article model)
        {
            _database.Articles.Add(model);
            await _database.SaveChangesAsync();
        }

        public async Task<Article?> GetAsync(int Id)
        {
            return await _database.Articles.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task UpdateAsync(Article form)
        {
            _database.Articles.Update(form);
            await _database.SaveChangesAsync();
        }

        #endregion BaseArticleAction

        public async Task<IEnumerable<DisplayArticleModel>> GetAuthorArticlesAsync(string authorId, int Page = 0, int count = 10)
        {
            var dbRes = await _database.Articles.AsNoTracking()
                .Select(a => new { a.AuthorId, a.AuthorUserName, a.Title, a.Description, a.Id, a.DateOfPublishing })
                .Where(a => a.AuthorId == authorId)
                .Take(count)
                .Skip(Page * count)
                .ToArrayAsync();

            List<DisplayArticleModel> result = new();
            foreach (var item in dbRes)
            {
                if (item != null)
                {
                    result.Add(new()
                    {
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorUserName,
                        Title = item.Title,
                        Description = item.Description,
                        Id = item.Id,
                        PubleshedTime = item.DateOfPublishing,
                    });
                }
            }
            return result.ToList();
        }

        public async Task<AnalyticModel> GetArticleAnaliticsAsync(int articleId)
        {
            var dbRes = await _database.ArticleViewers.AsNoTracking()
                .Include(x => x.Article)
                .Select(av => new
                {
                    av.Article.AuthorId,
                    av.ArticleId,
                    av.Article.Title,
                    av.Article.Description,
                    Views = _database.ArticleViewers
                        .Where(a => a.ArticleId == articleId)
                        .Count(),
                    UnicalViews = _database.ArticleViewers
                        .Where(a => a.ArticleId == articleId)
                        .GroupBy(a => a.ViewerId)
                        .Count(),
                    Today = _database.ArticleViewers
                        .Where(_a => _a.ArticleId == articleId && _a.ViewTime.Day == DateTime.UtcNow.Day)
                        .Count(),
                    LastHour = _database.ArticleViewers
                        .Where(_a => _a.ArticleId == articleId && _a.ViewTime.Hour >= DateTime.UtcNow.Hour - 1)
                        .Count()
                }).FirstOrDefaultAsync();

            return new()
            {
                AuthorId = dbRes!.AuthorId ?? "NO AUTHOR",
                ArticleId = articleId,
                Views = dbRes!.Views,
                UnicalViews = dbRes!.UnicalViews,
                Today = dbRes!.Today,
                LastHour = dbRes!.LastHour,
                Article = new()
                {
                    Title = dbRes!.Title ?? "NO TITLE",
                    Description = dbRes!.Description ?? "NO DESCRIPTION",
                    Id = articleId,
                },
            };
        }

        #region Recently

        public async Task<RecentlyViewModel> GetRecentlyAsync(int count)
        {
            return new()
            {
                Spotlight = await SpotlightAsync(),
                ArticleList = await RecentlyAsync(count)
            };
        }

        private async Task<DisplayArticleModel> SpotlightAsync()
        {
            var spotlightDb = await _database.Articles.AsNoTracking()
                .Select(a => new { a.Id, a.Title, a.Description, a.AuthorUserName, a.AuthorId, a.DateOfPublishing })
                .FirstOrDefaultAsync(a => a.Id ==
                    _database.ArticleViewers
                    .AsNoTracking()
                    .Where(a => a.ViewTime.Hour >= DateTime.UtcNow.Hour - 1 && a.Article.IsPubleshed)
                    .GroupBy(a => a.Id)
                    .Select(a => new { Id = a.Key, Count = a.Count() })
                    .OrderByDescending(a => a.Count)
                    .FirstOrDefault()!.Id);

            return new()
            {
                AuthorId = spotlightDb?.AuthorId ?? "NoAuthor",
                AuthorName = spotlightDb?.AuthorUserName ?? "NoAuthor",
                Title = spotlightDb?.Title ?? "No spotlight",
                Description = spotlightDb?.Description ?? "No description",
                PubleshedTime = spotlightDb?.DateOfPublishing ?? DateTime.UtcNow
            };
        }

        private async Task<IEnumerable<DisplayArticleModel>> RecentlyAsync(int count)
        {
            var recentlyDB = await _database.Articles.AsNoTracking()
                .Select(a => new { a.AuthorId, a.AuthorUserName, a.Id, a.Title, a.Description, a.DateOfPublishing, a.IsPubleshed })
                .Where(a => a.IsPubleshed)
                .OrderByDescending(a => a.DateOfPublishing)
                .Take(count)
                .ToArrayAsync();

            List<DisplayArticleModel> result = new();
            foreach (var item in recentlyDB)
            {
                if (item != null)
                {
                    result.Add(new()
                    {
                        AuthorId = item.AuthorId,
                        AuthorName = item.AuthorUserName,
                        Title = item.Title,
                        Description = item.Description,
                        PubleshedTime = item.DateOfPublishing,
                        Id = item.Id,
                    });
                }
            }

            return result.ToList();
        }

        #endregion Recently

        #region History

        public async Task<IEnumerable<ArticleViewers>> GetUserHistoryAsync(string userId, int Page = 0, int count = 10)
        {
            return await _database.ArticleViewers
                .Include(av => av.Article)
                .Where(av => av.ViewerId == userId)
                .OrderByDescending(av => av.ViewTime)
                .Take(count)
                .Skip(Page * count)
                .ToListAsync();
        }

        public async Task AddToUserHistoryAsync(string userId, int articleId)
        {
            var viewer = new ArticleViewers()
            {
                ArticleId = articleId,
                ViewerId = userId,
                ViewTime = DateTime.UtcNow,
            };
            _database.ArticleViewers.Add(viewer);
            await _database.SaveChangesAsync();
        }

        #endregion History
    }
}
