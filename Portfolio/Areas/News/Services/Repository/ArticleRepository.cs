using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
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
            return await _database.Articles.OrderByDescending(a => a.Id).Where(a => a.IsDeleted == false && a.IsPubleshed).Take(count).ToArrayAsync();
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
            return await _database.Articles.Where(a => a.AuthorId == authorId).ToArrayAsync();
        }
    }
}
