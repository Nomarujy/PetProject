using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.News.Controls
{
    public class ReadController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Post(int Id)
        {
            return View();
        }
    }
}
