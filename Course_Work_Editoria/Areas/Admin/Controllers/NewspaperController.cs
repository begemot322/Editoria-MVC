using Course_Work_Editoria.Data;
using Course_Work_Editoria.Models;
using Course_Work_Editoria.Models.VIewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Course_Work_Editoria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewspaperController : Controller
    {

        private readonly ApplicationDbContext _db;

        public NewspaperController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var newspaperList = _db.Newspapers
             .Include(n => n.Editor)
             .ToList();
            return View(newspaperList);
        }

        public IActionResult Upsert(int? newspaperId)
        {
            var viewModel = new NewspaperVM
            {
                Editors = _db.Editors.ToList().Select(e => new SelectListItem
                {
                    Text = $"{e.Name} {e.Surname}",
                    Value = e.EditorId.ToString()
                }),
                Newspaper = new Newspaper()
            };
            if (newspaperId == null || newspaperId == 0)
            {
                //create
                return View(viewModel);
            }
            else
            {
                //update
                viewModel.Newspaper = _db.Newspapers.FirstOrDefault(n => n.NewspaperId == newspaperId);
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(NewspaperVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.Newspaper.NewspaperId == 0)
                    {
                        _db.Newspapers.Add(viewModel.Newspaper);
                        _db.SaveChanges();
                        TempData["success"] = "Газета успешно добавлена";
                    }
                    else
                    {
                        _db.Newspapers.Update(viewModel.Newspaper);
                        _db.SaveChanges();
                        TempData["success"] = "Газета успешно обновлена";
                    }
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при сохранении данных. Нарушена связь с редактором.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Произошла неизвестная ошибка: " + ex.Message);
                }
            }

            viewModel.Editors = _db.Editors.ToList().Select(e => new SelectListItem
            {
                Text = $"{e.Name} {e.Surname}",
                Value = e.EditorId.ToString()
            });
            return View(viewModel);
        }

        public IActionResult Delete(int newspaperId)
        {
            if (newspaperId == 0)
            {
                return NotFound();
            }

            var newspaperFromDb = _db.Newspapers.Include(n => n.Editor).FirstOrDefault(n => n.NewspaperId == newspaperId);
            if (newspaperFromDb == null)
            {
                return NotFound();
            }
            return View(newspaperFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int newspaperId)
        {
            var newspaper = _db.Newspapers.FirstOrDefault(n => n.NewspaperId == newspaperId);

            if (newspaper == null)
            {
                return NotFound();
            }

            _db.Newspapers.Remove(newspaper);
            _db.SaveChanges();
            TempData["success"] = "Газета успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
