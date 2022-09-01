using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Models.ViewModel.Home;
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
        public async Task<IActionResult> Index() => View(await _database.GetRecentlyAsync());


        [HttpGet]
        public async Task<IActionResult> Read(int Id)
        {
            var article = await _database.GetAsync(Id);
            if (article != null)
            {
                var result = await _authorizationService.AuthorizeAsync(User, article, "News_Read");
                if (result.Succeeded)
                {
                    string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    if (userId != null)
                    {
                        await _database.AddToUserHistoryAsync(userId, article.Id);
                    }

                    ReadViewModel model = new()
                    {
                        Recently = await _database.GetRecentlyAsync(),
                        Article = article,
                    };
                    return View(model);
                }
                else
                {
                    return Forbid();
                }
            }
            return NotFound();
        }

        [Authorize, HttpGet]
        public async Task<IActionResult> History()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var history = await _database.GetUserHistoryAsync(userId);
                return View(history);
            }
            return Forbid();
        }
    }
}
