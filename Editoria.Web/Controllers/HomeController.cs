using System.Diagnostics;
using Editoria.Application.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Editoria.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var articlesWithNewspapers = await _articleService.GetAllArticlesAsync();
            return View(articlesWithNewspapers);
        }
        public async Task<IActionResult> Details(int articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);
            return View(article);
        }
    }
}
