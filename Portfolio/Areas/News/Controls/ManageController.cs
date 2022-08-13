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

		public ManageController(IArticleRepository repository, IAuthorizationService authorization)
		{
			_database = repository;
			_authorization = authorization;
		}

		[HttpGet]
		public IActionResult Create() => View();

		[HttpGet]
		public async Task<IActionResult> MyArticles()
		{
			string authorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var model = await _database.GetArticlesByAuthorIdAsync(authorId);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Update(int Id)
		{
			Article? article = await _database.FindByIdAsync(Id);
			if (article != null)
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
			return NotFound();
		}

		[HttpPost]
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

				await _database.AddArticleAsync(article);
				return RedirectToAction("Index", "Read", new { area = "News" });
			}
			return View(form);
		}

		[HttpPost]
		public async Task<IActionResult> Update(UpdateArticleForm form)
		{
			if (ModelState.IsValid)
			{
				Article? article = await _database.FindByIdAsync(form.Id);
				if (article == null) return NotFound();

				var result = await _authorization.AuthorizeAsync(User, article, "News_Update");

				if (result.Succeeded)
				{
					article.Title = form.Title;
					article.Description = form.Description;
					article.Content = form.Content;
					article.IsPubleshed = form.IsPubleshed;
					if (form.IsPubleshed) article.DateOfPublishing = DateTime.UtcNow;

					await _database.UpdateArticleAsync(article);

					return RedirectToAction("Article", "Read", new { area = "News", form.Id });
				}
				else
				{
					return Forbid();
				}
			}
			return View(form);
		}
	}
}
