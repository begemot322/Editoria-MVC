using Course_Work_Editoria.Services.File;
using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Editoria.Models.Entities;
using Editoria.Models.ViewModel ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_Work_Editoria.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly FileService _fileService;

        public ArticleController(IArticleRepository articleRepository, FileService fileService)
        {
            _articleRepository = articleRepository;
            _fileService = fileService;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            var articles = _articleRepository.GetAllArticles();
            return View(articles);
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Details(int articleId)
        {
            var article = _articleRepository.GetArticleById(articleId);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(int? articleId)
        {
            var viewModel = new ArticleVM
            {
                Issues = _articleRepository.GetIssueSelectList(),
                Categories = _articleRepository.GetCategorySelectList(),
                Authors = _articleRepository.GetAuthorSelectList(),
                Article = articleId.HasValue?
                    _articleRepository.GetArticleById(articleId.Value):new Article(),
                Tags = _articleRepository.GetTagSelectList(),
                SelectedTags = articleId.HasValue?
                    _articleRepository.GetArticleById(articleId.Value)
                    .ArticleTags.Select(at=>at.TagId).ToList(): new List<int>()
            };

            if (articleId.HasValue && viewModel.Article == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(ArticleVM viewModel, IFormFile? imageFile)
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
                    _articleRepository.AddArticle(viewModel.Article, viewModel.SelectedTags);
                    TempData["success"] = "Статья успешно добавлена";
                }

                else
                {
                    _articleRepository.UpdateArticle(viewModel.Article, viewModel.SelectedTags);
                    TempData["success"] = "Статья успешно обновлена";
                }

                return RedirectToAction("Index");
            }

            viewModel.Issues = _articleRepository.GetIssueSelectList();
            viewModel.Categories = _articleRepository.GetCategorySelectList();
            viewModel.Authors = _articleRepository.GetAuthorSelectList();
            viewModel.Tags = _articleRepository.GetTagSelectList();

            return View(viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int articleId)
        {
            var article = _articleRepository.GetArticleById(articleId);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeletePOST(int articleId)
        {
            var article = _articleRepository.GetArticleById(articleId);

            if (article != null)
            {
                if (!string.IsNullOrEmpty(article.ImageUrl))
                {
                    _fileService.DeleteFile(article.ImageUrl);
                }
                _articleRepository.DeleteArticle(articleId);
            }


            TempData["success"] = "Статья успешно удалена";
            return RedirectToAction("Index");
        }

    }
}
