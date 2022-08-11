using Microsoft.EntityFrameworkCore;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.FormModel;
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
            try
            {
                return await _database.Articles.FirstOrDefaultAsync(a => a.Id == Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Article>> GetRecentlyAsync(int count)
        {
            return await _database.Articles.OrderByDescending(a => a.Id).Where(a => a.IsDeleted == false && a.IsPubleshed).Take(count).ToArrayAsync();
        }

        public async Task AddArticleAsync(Article model)
        {
            try
            {
                await _database.Articles.AddAsync(model);
                await _database.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }

        public async Task UpdateArticleAsync(Article article)
        {
            try
            {
                _database.Articles.Update(article);
                await _database.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
        }
    }
}
