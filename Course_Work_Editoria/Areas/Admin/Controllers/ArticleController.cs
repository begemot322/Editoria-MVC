using Course_Work_Editoria.Data;
using Course_Work_Editoria.Models;
using Course_Work_Editoria.Models.VIewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_Work_Editoria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ArticleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var articles = _db.Articles
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Author)
                .ToList();
            return View(articles);
        }

        public IActionResult Upsert(int? articleId)
        {
            var viewModel = new ArticleVM
            {
                Issues = _db.Issues.Select(i => new SelectListItem
                {
                    Text = $"Выпуск Id:{i.IssueId.ToString()}",
                    Value = i.IssueId.ToString()
                }).ToList(),
                Categories = _db.Categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.CategoryId.ToString()
                }).ToList(),
                Authors = _db.Authors.Select(a => new SelectListItem
                {
                    Text = $"{a.Name} {a.Surname}",
                    Value = a.AuthorId.ToString()
                }).ToList(),
                Article = new Article()
            };

            if (articleId == null || articleId == 0)
            {
                return View(viewModel);
            }
            else
            {
                var articleFromDb = _db.Articles.FirstOrDefault(a => a.ArticleId == articleId);
                if (articleFromDb == null)
                {
                    return NotFound();
                }

                viewModel.Article = articleFromDb;
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ArticleVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Article.ArticleId == 0)
                {
                    _db.Articles.Add(viewModel.Article);
                    TempData["success"] = "Статья успешно добавлена";
                }
                else
                {
                    _db.Articles.Update(viewModel.Article);
                    TempData["success"] = "Статья успешно обновлена";
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.Issues = _db.Issues.Select(i => new SelectListItem
            {
                Text = i.IssueId.ToString(),
                Value = i.IssueId.ToString()
            }).ToList();

            viewModel.Categories = _db.Categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();

            viewModel.Authors = _db.Authors.Select(a => new SelectListItem
            {
                Text = a.Name,
                Value = a.AuthorId.ToString()
            }).ToList();

            return View(viewModel);
        }

        public IActionResult Delete(int articleId)
        {
            var articleFromDb = _db.Articles
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Author)
                .FirstOrDefault(a => a.ArticleId == articleId);

            if (articleFromDb == null)
            {
                return NotFound();
            }

            return View(articleFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int articleId)
        {
            var article = _db.Articles.FirstOrDefault(a => a.ArticleId == articleId);
            if (article == null)
            {
                return NotFound();
            }

            _db.Articles.Remove(article);
            _db.SaveChanges();
            TempData["success"] = "Статья успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
