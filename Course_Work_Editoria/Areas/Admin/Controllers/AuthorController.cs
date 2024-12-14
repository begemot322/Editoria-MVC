using Course_Work_Editoria.Data;
using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var AuthorList = _db.Authors.ToList();
            return View(AuthorList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Add(author);
                _db.SaveChanges();
                TempData["success"] = "Автор успешно создан";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int AuthorId)
        {
            if (AuthorId == 0)
            {
                return NotFound();
            }
            Author authorFromDb = _db.Authors.FirstOrDefault(a => a.AuthorId == AuthorId);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Authors.Update(author);
                _db.SaveChanges();
                TempData["success"] = "Автор успешно обновлён";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int AuthorId)
        {
            if (AuthorId == 0)
            {
                return NotFound();
            }
            Author authorFromDb = _db.Authors.FirstOrDefault(a => a.AuthorId == AuthorId);
            if (authorFromDb == null)
            {
                return NotFound();
            }
            return View(authorFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int AuthorId)
        {
            Author obj = _db.Authors.FirstOrDefault(a => a.AuthorId == AuthorId);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Authors.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Автор успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
