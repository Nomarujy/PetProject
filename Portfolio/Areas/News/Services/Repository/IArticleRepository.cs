﻿using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel;

namespace Portfolio.Areas.News.Services.Repository
{
    public interface IArticleRepository
    {
        Task AddArticleAsync(Article model);
        Task<Article?> FindByIdAsync(int Id);
        Task<IEnumerable<Article>> GetRecentlyAsync(int count);

        Task<IEnumerable<Article>> GetArticlesByAuthorIdAsync(string authorId);
        Task UpdateArticleAsync(Article article);

        Task WriteToHistory(string userId, int articleId);
        Task<IEnumerable<ArticleViewers>> GetUserHistory(string userId);

        AnaliticModel GetAnaliticsById(int Id);
    }
}