using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Editoria.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Editoria.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index(int? minPriority, int? maxPriority)
        {
            IEnumerable<Category> categoryList;

            if (minPriority.HasValue && maxPriority.HasValue)
            {
                categoryList = await _categoryService.GetCategoriesByPriorityAsync(minPriority.Value, maxPriority.Value);
            }
            else
            {
                categoryList = await _categoryService.GetAllCategoriesAsync();
            }
            return View(categoryList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(int? categoryId)
        {
            var category = categoryId.HasValue ?
                await _categoryService.GetCategoryByIdAsync(categoryId.Value) : new Category();

            if (categoryId.HasValue && category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                    await _categoryService.CreateCategoryAsync(category);
                    TempData["success"] = "Категория создана успешно!";
                }
                else
                {
                    await _categoryService.UpdateCategoryAsync(category);
                    TempData["success"] = "Категория обновлена успешно!";
                }
                return RedirectToAction("Index");
            }
            return View(category);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int categoryId)
        {
            var category = await _categoryService.GetCategoryByIdAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);

            TempData["success"] = "Категория удалена успешно!";
            return RedirectToAction("Index");
        }
    }
}
