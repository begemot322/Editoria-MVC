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
        public async Task<IActionResult> Upsert(int? articleId)
        {
            var viewModel = new ArticleVM
            {
                Issues = await _dropdownService.GetIssueSelectListAsync(),
                Categories = await _dropdownService.GetCategorySelectListAsync(),
                Authors = await _dropdownService.GetAuthorSelectListAsync(),
                Article = articleId.HasValue ?
                    await _articleService.GetArticleByIdAsync(articleId.Value) : new Article(),
                Tags = await _dropdownService.GetTagSelectListAsync(),
                SelectedTags = articleId.HasValue ?
                   (await _articleService.GetArticleByIdAsync(articleId.Value))
                    .ArticleTags.Select(at => at.TagId).ToList() : new List<int>()
            };

            if (articleId.HasValue && viewModel.Article == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(ArticleVM viewModel, IFormFile? imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null)
                {
                    string imageUrl = _fileService.SaveFile(imageFile, "images/articles");

                    if (!string.IsNullOrEmpty(viewModel.Article.ImageUrl))
                    {
                        _fileService.DeleteFile(viewModel.Article.ImageUrl);
                    }
                    viewModel.Article.ImageUrl = imageUrl;
                }

                if (viewModel.Article.ArticleId == 0)
                {
                    await _articleService.CreateArticleAsync(viewModel.Article, viewModel.SelectedTags);
                    TempData["success"] = "Статья успешно добавлена";
                }

                else
                {
                    await _articleService.UpdateArticleAsync(viewModel.Article, viewModel.SelectedTags);
                    TempData["success"] = "Статья успешно обновлена";
                }

                return RedirectToAction("Index");
            }

            viewModel.Issues = await _dropdownService.GetIssueSelectListAsync();
            viewModel.Categories = await _dropdownService.GetCategorySelectListAsync();
            viewModel.Authors = await _dropdownService.GetAuthorSelectListAsync();
            viewModel.Tags = await _dropdownService.GetTagSelectListAsync();

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

    }
}
