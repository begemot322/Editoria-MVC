using Editoria.Data.Context;
using Editoria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var CategoryList = _db.Categories.ToList();
            return View(CategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["success"] = "Категория создалась успешно";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int CategoryId)
        {
            if (CategoryId == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["success"] = "Категория изменилась успешно";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int CategoryId)
        {
            if (CategoryId == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int CategoryId)
        {
            Category obj = _db.Categories.FirstOrDefault(c => c.CategoryId == CategoryId);

            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Категория удалилась успешно";
            return RedirectToAction("Index");
        }
    }
}
