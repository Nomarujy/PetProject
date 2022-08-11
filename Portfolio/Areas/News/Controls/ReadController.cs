using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel;
using Portfolio.Areas.News.Services.Repository;

namespace Portfolio.Areas.News.Controls
{
	[Area("News")]
	public class ReadController : Controller
	{
		private readonly IArticleRepository _database;
		private readonly IAuthorizationService _authorizationService;

		public ReadController(IArticleRepository articleRepository, IAuthorizationService authorizationService)
		{
			_database = articleRepository;
			_authorizationService = authorizationService;
		}

		[HttpGet]
		public async Task<IActionResult> Index() => View(await GetViewModel());


        [HttpGet]
        public async Task<IActionResult> Article(int Id)
		{
			var article = await _database.FindByIdAsync(Id);
			if (article != null)
			{
				var result = await _authorizationService.AuthorizeAsync(User, article, "News_Read");
				if (result.Succeeded)
				{
                    return View(await GetViewModel(article));
                }
				else
				{
					return Forbid();
				}
            }
            return NotFound();
        }

        private async Task<NewsViewModel> GetViewModel(Article? article = null)
		{
			var recentlyPost = await _database.GetRecentlyAsync(5);
			Article? spotlight = null;
			return new NewsViewModel(recentlyPost, spotlight, article);
		}
	}
}
