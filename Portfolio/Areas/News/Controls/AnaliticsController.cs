using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Services.Repository;

namespace Portfolio.Areas.News.Controls
{
    [Area("News")]
    public class AnaliticsController : Controller
    {
        private readonly IArticleRepository _database;

        public AnaliticsController(IArticleRepository database)
        {
            _database = database;
        }

        public IActionResult Article(int Id)
        {
            var model = _database.GetAnaliticsById(Id);
            return View(model);
        }
    }
}
