using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Services.Repository;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Area("News"), Authorize]
    public class AnalyticsController : Controller
    {
        private readonly IArticleRepository _database;
        private readonly IAuthorizationService _authorization;

        public AnalyticsController(IArticleRepository database, IAuthorizationService authorization)
        {
            _database = database;
            _authorization = authorization;
        }

        [HttpGet]
        public async Task<IActionResult> MyArticles()
        {
            string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _database.GetArticlesByAuthorIdAsync(authorId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Article(int Id)
        {
            var model = await _database.GetAnaliticsByIdAsync(Id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (model == null)
            {
                Forbid();
            }

            return View(model);
        }
    }
}
