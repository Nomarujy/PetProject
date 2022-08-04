using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Data.Post;

namespace Portfolio.Areas.News.Controls
{
    public class ReadController : Controller
    {
        private IPostRepository database;

        public ReadController(IPostRepository database)
        {
            this.database = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Post(int Id)
        {
            var model = database.GetPostWithAuthor(Id);

            if (model == null) return BadRequest("Post not found");

            return View(model);
        }
    }
}
