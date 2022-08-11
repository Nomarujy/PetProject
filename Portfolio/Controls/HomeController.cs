using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Controls
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
