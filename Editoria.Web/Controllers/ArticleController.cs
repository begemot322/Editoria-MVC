using Course_Work_Editoria.Services.File;
using Editoria.Application.Services.Implementation;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Editoria.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Editoria.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly DropdownDataService _dropdownService;
        private readonly IFileService _fileService;

        public ArticleController(
            IArticleService articleService,
            IFileService fileService,
            DropdownDataService dropdownService)
        {
            _articleService = articleService;
            _fileService = fileService;
            _dropdownService = dropdownService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticlesAsync();
            return View(articles);
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return View(new List<Article>());
            }

            var articles = await _articleService.SearchArticlesAsync(keyword);
            return View(articles);
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Details(int articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new ArticleVM
            {
                Issues = await _dropdownService.GetIssueSelectListAsync(),
                Categories = await _dropdownService.GetCategorySelectListAsync(),
                Authors = await _dropdownService.GetAuthorSelectListAsync(),
                Tags = await _dropdownService.GetTagSelectListAsync(),
                Article = new Article(),
                SelectedTags = new List<int>()
            };

            return View(viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        [HttpPost]
        public async Task<IActionResult> Create(ArticleVM viewModel, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    viewModel.Article.ImageUrl = _fileService.SaveFile(imageFile, "images/articles");
                }

                await _articleService.CreateArticleAsync(viewModel.Article, viewModel.SelectedTags);
                TempData["success"] = "Статья успешно добавлена";
                return RedirectToAction("Index");
            }

            await ReloadDropdowns(viewModel);

            return View(viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);
            if (article == null) return NotFound();

            var viewModel = new ArticleVM
            {
                Issues = await _dropdownService.GetIssueSelectListAsync(),
                Categories = await _dropdownService.GetCategorySelectListAsync(),
                Authors = await _dropdownService.GetAuthorSelectListAsync(),
                Tags = await _dropdownService.GetTagSelectListAsync(),
                Article = article,
                SelectedTags = article.ArticleTags.Select(at => at.TagId).ToList()
            };

            return View(viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        [HttpPost]
        public async Task<IActionResult> Update(ArticleVM viewModel, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    if (!string.IsNullOrEmpty(viewModel.Article.ImageUrl))
                    {
                        _fileService.DeleteFile(viewModel.Article.ImageUrl);
                    }
                    viewModel.Article.ImageUrl = _fileService.SaveFile(imageFile, "images/articles");
                }

                await _articleService.UpdateArticleAsync(viewModel.Article, viewModel.SelectedTags);
                TempData["success"] = "Статья успешно обновлена";
                return RedirectToAction("Index");
            }

            await ReloadDropdowns(viewModel);

            return View(viewModel);
        }


        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int articleId)
        {
            var article = await _articleService.GetArticleByIdAsync(articleId);

            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImageUrl))
                {
                    _fileService.DeleteFile(article.ImageUrl);
                }
                await _articleService.DeleteArticleAsync(articleId);
            }

            TempData["success"] = "Статья успешно удалена";
            return RedirectToAction("Index");
        }
        private async Task ReloadDropdowns(ArticleVM viewModel)
        {
            viewModel.Issues = await _dropdownService.GetIssueSelectListAsync();
            viewModel.Categories = await _dropdownService.GetCategorySelectListAsync();
            viewModel.Authors = await _dropdownService.GetAuthorSelectListAsync();
            viewModel.Tags = await _dropdownService.GetTagSelectListAsync();
        }

    }
}
