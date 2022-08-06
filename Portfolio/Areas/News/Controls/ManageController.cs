using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Data.Post.Repository;
using Portfolio.Areas.News.Models.Post;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IPostRepository database;
        private readonly ILogger logger;

        public ManageController(IPostRepository database, ILogger<ManageController> logger)
        {
            this.database = database;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var model = database.FindFirstPost(Id);

            if (model == null) return BadRequest("Новости не существует");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostForm form)
        {
            string? email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (ModelState.IsValid && email != null)
            {
                PostModel model = new(form, email)
                {
                    EditedTime = DateTime.UtcNow
                };
                database.Add(model);

                logger.LogInformation("Created news by user {Email}", email);
                return RedirectToAction("Index", "Read");
            }
            return BadRequest("Form not valid");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostForm form)
        {
            var postInDb = database.FindFirstPost(form.Id);

            if (postInDb == null) return BadRequest("Post not found");

            postInDb.Title = form.Title;
            postInDb.Content = form.Content;
            postInDb.IsPubleched = form.IsPubleched;
            postInDb.EditedTime = DateTime.UtcNow;

            database.Update(postInDb);

            return RedirectToAction("Post", "Read", new { area = "News", form.Id });
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            var postInDb = database.FindFirstPost(Id);

            if (postInDb == null) return BadRequest("Post not found");

            // TO DO Check Access

            postInDb.IsDeleted = true;
            database.Update(postInDb);

            return View();
        }
    }
}
