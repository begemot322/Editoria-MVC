using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            var categoryList = _categoryRepository.GetAllCategories();
            return View(categoryList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(int? categoryId)
        {
            var category = categoryId.HasValue ?
                _categoryRepository.GetCategoryById(categoryId.Value) : new Category();

            if (categoryId.HasValue && category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    _categoryRepository.AddCategory(category);
                    TempData["success"] = "Категория создана успешно!";
                }
                else
                {
                    _categoryRepository.UpdateCategory(category);
                    TempData["success"] = "Категория обновлена успешно!";
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int categoryId)
        {
            var category = _categoryRepository.GetCategoryById(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeletePOST(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);

            TempData["success"] = "Категория удалена успешно!";
            return RedirectToAction("Index");
        }
    }
}
