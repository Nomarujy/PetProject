using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Data.Post.Repository;
using Portfolio.Areas.News.Models.Post;

namespace Portfolio.Areas.News.Controls
{
    public class ReadController : Controller
    {
        private readonly IPostRepository database;
        private readonly ISpotlightService spotlight;

        public ReadController(IPostRepository database, ISpotlightService spotlight)
        {
            this.database = database;
            this.spotlight = spotlight;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewModel model = CreateBaseModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Post(int Id)
        {
            var Post = database.GetPostWithAuthor(Id);
            if (Post == null) return BadRequest("Post not found");

            if (Post.IsPubleched == false || Post.IsDeleted) return BadRequest("Post not publeshed"); //TO DO if Authorized(admin, author), return page

            ViewModel model = CreateBaseModel();
            model.CurrentPost = Post;

            spotlight.PostRequested(Id);
            return View(model);
        }

        private ViewModel CreateBaseModel()
        {
            ViewModel model = new()
            {
                RecentlyPosts = database.GetRecentlyPosts(),
                Spotlight = database.FindFirstPost(spotlight.CurrentSpotlight)
            };

            return model;
        }
    }
}
