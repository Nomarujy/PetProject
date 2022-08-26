using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Services.Repository;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Area("News")]
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
            var model = await _database.GetArticlesByAuthorIdAsync(authorId);
            return View(model);
        }

        [HttpGet]
        public IActionResult Article(int Id)
        {
            var model = _database.GetAnaliticsById(Id);
            return View(model);
        }
    }
}
