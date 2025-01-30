 using System.Diagnostics;
using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Editoria.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Course_Work_Editoria.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleRepository _articleRepository;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var articlesWithNewspapers = _articleRepository.GetAllArticles();
            return View(articlesWithNewspapers);
        }
        public IActionResult Details(int articleId)
        {
            var article = _articleRepository.GetArticleById(articleId);
            return View(article);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
