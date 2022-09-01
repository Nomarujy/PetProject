using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.FormModel;
using Portfolio.Areas.News.Services.Repository;
using System.Security.Claims;

namespace Portfolio.Areas.News.Controls
{
    [Authorize, Area("News")]
    public class ManageController : Controller
    {
        private readonly IArticleRepository _database;
        private readonly IAuthorizationService _authorization;
        private readonly ILogger<ManageController> _logger;

        public ManageController(IArticleRepository repository, IAuthorizationService authorization, ILogger<ManageController> logger)
        {
            _database = repository;
            _authorization = authorization;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpGet, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Update(int Id)
        {
            Article? article = await _database.GetAsync(Id);
            if (article != null)
            {
                if (await UserHaveAccess(article))
                {
                    UpdateArticleForm model = new()
                    {
                        Id = article.Id,
                        Title = article.Title,
                        Description = article.Description,
                        Content = article.Content,
                        IsPubleshed = article.IsPubleshed
                    };
                    return View(model);
                }
                else
                {
                    Forbid();
                }
            }
            return NotFound();
        }

        [HttpPost, AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateArticleForm form)
        {
            if (ModelState.IsValid)
            {
                Article article = new()
                {
                    AuthorId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    AuthorUserName = User.Identity?.Name!,
                    Title = form.Title,
                    Description = form.Description,
                    Content = form.Content,
                    IsPubleshed = form.IsPubleshed,
                    DateOfPublishing = DateTime.UtcNow,
                };

                _logger.LogInformation("Created article: {Title}, by user: {userId}.", article.Title, article.AuthorId);
                await _database.AddArticleAsync(article);
                return RedirectToAction("MyArticles", "Analytics");
            }
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateArticleForm form)
        {
            if (ModelState.IsValid)
            {
                Article? article = await _database.GetAsync(form.Id);
                if (article == null) return NotFound();

                if (await UserHaveAccess(article))
                {
                    {
                        article.Title = form.Title;
                        article.Description = form.Description;
                        article.Content = form.Content;
                        article.IsPubleshed = form.IsPubleshed;
                        if (form.IsPubleshed) article.DateOfPublishing = DateTime.UtcNow;
                    }

                    _logger.LogInformation("Updated article with id: {articleId}", article.Id);
                    await _database.UpdateAsync(article);

                    return RedirectToAction("MyArticles", "Analytics", new { area = "News" });
                }
                else
                {
                    _logger.LogInformation("Forbid update article with id: {articleId}, for user {name}", article.Id, User.Identity?.Name ?? "Anonymous");
                    return Forbid();
                }
            }
            return View(form);
        }

        private async Task<bool> UserHaveAccess(Article article)
        {
            var result = await _authorization.AuthorizeAsync(User, article, "News_Manage");
            return result.Succeeded;
        }
    }
}
