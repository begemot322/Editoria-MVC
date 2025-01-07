using Editoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editoria.Models.ViewModel;
using Editoria.Data.Context;

namespace Course_Work_Editoria.Controllers
{
    public class IssueController : Controller
    {
        private readonly ApplicationDbContext _db;

        public IssueController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var issueList = _db.Issues
                .Include(i => i.Newspaper)
                .ToList();
            return View(issueList);
        }

        public IActionResult Upsert(int? issueId)
        {
            var viewModel = new IssueVM
            {
                Newspapers = _db.Newspapers.ToList().Select(n => new SelectListItem
                {
                    Text = n.Name,
                    Value = n.NewspaperId.ToString()
                }),
                Issue = new Issue()
            };

            if (issueId == null || issueId == 0)
            {
                // Create
                return View(viewModel);
            }
            else
            {
                // Update
                viewModel.Issue = _db.Issues.FirstOrDefault(i => i.IssueId == issueId);
                if (viewModel.Issue == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(IssueVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.Issue.IssueId == 0)
                    {
                        _db.Issues.Add(viewModel.Issue);
                        TempData["success"] = "Выпуск успешно добавлен";
                        _db.SaveChanges();
                    }
                    else
                    {
                        _db.Issues.Update(viewModel.Issue);
                        TempData["success"] = "Выпуск успешно обновлён";
                        _db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при сохранении данных. Нарушена связь с газетой.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Произошла неизвестная ошибка: " + ex.Message);
                }
            }

            viewModel.Newspapers = _db.Newspapers.ToList().Select(n => new SelectListItem
            {
                Text = n.Name,
                Value = n.NewspaperId.ToString()
            });
            return View(viewModel);
        }


        public IActionResult Delete(int issueId)
        {
            var issueFromDb = _db.Issues.Include(i => i.Newspaper).FirstOrDefault(i => i.IssueId == issueId);
            if (issueFromDb == null)
            {
                return NotFound();
            }
            return View(issueFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int issueId)
        {
            var issue = _db.Issues.FirstOrDefault(i => i.IssueId == issueId);
            if (issue == null)
            {
                return NotFound();
            }

            _db.Issues.Remove(issue);
            _db.SaveChanges();
            TempData["success"] = "Выпуск успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
