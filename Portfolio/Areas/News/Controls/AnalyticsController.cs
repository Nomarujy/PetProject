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

        public AnalyticsController(IArticleRepository database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> MyArticles()
        {
            string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await _database.GetAuthorArticlesAsync(authorId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Article(int Id)
        {
            var model = await _database.GetArticleAnaliticsAsync(Id);

            if (model == null)
            {
                Forbid();
            }

            return View(model);
        }
    }
}
