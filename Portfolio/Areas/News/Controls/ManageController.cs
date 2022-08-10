using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Portfolio.Areas.News.Data.Post.Repository;
using Portfolio.Areas.News.Models.Post;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IPostRepository database;
        private readonly IAuthorizationService authorizationService;
        private readonly ILogger logger;

        public ManageController(IPostRepository database, IAuthorizationService authorizationService, ILogger<ManageController> logger)
        {
            this.database = database;
            this.authorizationService = authorizationService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int Id)
        {
            var model = database.FindFirstPost(Id);

            if (model == null) return BadRequest("Новости не существует");

            if (await UserHaveAccess(model))
            {
                return View(model);
            }
            else
            {
                return Forbid();
            }
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
        public async Task<ActionResult> Edit(PostForm form)
        {
            var postInDb = database.FindFirstPost(form.Id);

            if (postInDb == null) return BadRequest("Post not found");

            postInDb.Title = form.Title;
            postInDb.Content = form.Content;
            postInDb.IsPubleched = form.IsPubleched;
            postInDb.EditedTime = DateTime.UtcNow;

            if (await UserHaveAccess(postInDb))
            {
                database.Update(postInDb);
                return RedirectToAction("Post", "Read", new { area = "News", postInDb.Id });
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var postInDb = database.FindFirstPost(Id);

            if (postInDb == null) return BadRequest("Post not found");
            postInDb.IsDeleted = true;

            if (await UserHaveAccess(postInDb))
            {
                database.Update(postInDb);
                return RedirectToAction("Index", "Read", new { area = "News" });
            }
            else
            {
                return Forbid();
            }
        }

        public async Task<bool> UserHaveAccess(PostModel Post)
        {
            var AuthRes = await authorizationService.AuthorizeAsync(User, Post, "PostPermision");

            if (AuthRes.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
