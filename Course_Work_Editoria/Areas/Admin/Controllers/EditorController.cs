using Course_Work_Editoria.Data;
using Course_Work_Editoria.Models;
using Course_Work_Editoria.Models.VIewModel;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EditorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EditorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string name, string email)
        {
            IQueryable<Editor> editors = _db.Editors;

            if (!string.IsNullOrEmpty(name))
            {
                editors = editors.Where(e => e.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                editors = editors.Where(e => e.Email.Contains(email));
            }

            EditorListViewModel viewModel = new EditorListViewModel
            {
                Editors = editors.ToList(),
                Name = name,
                Email = email
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Editor editor)
        {
            if (ModelState.IsValid)
            {
                _db.Editors.Add(editor);
                _db.SaveChanges();
                TempData["success"] = "Редактор успешно добавлен";
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        public IActionResult Edit(int editorId)
        {
            if (editorId == 0)
            {
                return NotFound();
            }

            var editorFromDb = _db.Editors.FirstOrDefault(e => e.EditorId == editorId);
            if (editorFromDb == null)
            {
                return NotFound();
            }
            return View(editorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Editor editor)
        {
            if (ModelState.IsValid)
            {
                _db.Editors.Update(editor);
                _db.SaveChanges();
                TempData["success"] = "Редактор успешно обновлён";
                return RedirectToAction("Index");
            }
            return View(editor);
        }

        public IActionResult Delete(int editorId)
        {
            if (editorId == 0)
            {
                return NotFound();
            }

            var editorFromDb = _db.Editors.FirstOrDefault(e => e.EditorId == editorId);
            if (editorFromDb == null)
            {
                return NotFound();
            }
            return View(editorFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int editorId)
        {
            var editor = _db.Editors.FirstOrDefault(e => e.EditorId == editorId);

            if (editor == null)
            {
                return NotFound();
            }

            _db.Editors.Remove(editor);
            _db.SaveChanges();
            TempData["success"] = "Редактор успешно удалён";
            return RedirectToAction("Index");
        }

    }
}
