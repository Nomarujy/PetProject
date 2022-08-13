using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel;
using Portfolio.Areas.News.Services.Repository;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Area("News")]
    public class HomeController : Controller
    {
        private readonly IArticleRepository _database;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(IArticleRepository articleRepository, IAuthorizationService authorizationService)
        {
            _database = articleRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index() => View(await GetViewModel());


        [HttpGet]
        public async Task<IActionResult> Read(int Id)
        {
            var article = await _database.FindByIdAsync(Id);
            if (article != null)
            {
                var result = await _authorizationService.AuthorizeAsync(User, article, "News_Read");
                if (result.Succeeded)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId != null)
                    {
                        await _database.WriteToHistory(userId, article.Id);
                    }

                    return View(await GetViewModel(article));
                }
                else
                {
                    return Forbid();
                }
            }
            return NotFound();
        }

        private async Task<NewsViewModel> GetViewModel(Article? article = null)
        {
            var recentlyPost = await _database.GetRecentlyAsync(5);
            Article? spotlight = null;
            return new NewsViewModel(recentlyPost, spotlight, article);
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> History()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var history = await _database.GetUserHistory(userId);
                return View(history);
            }
            return Forbid();
        }
    }
}
