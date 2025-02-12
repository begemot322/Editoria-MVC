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
    public class NewspaperController : Controller
    {
        private readonly INewspaperService _newspaperService;
        private readonly DropdownDataService _selectListService;

        public NewspaperController(INewspaperService newspaperService,
            DropdownDataService selectListService)
        {
            _newspaperService = newspaperService;
            _selectListService = selectListService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var newspaperList = await _newspaperService.GetAllNewspapersAsync();
            return View(newspaperList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new NewspaperVM
            {
                Editors = await _selectListService.GetEditorsSelectListAsync(),
                Newspaper = new Newspaper()
            };
            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(NewspaperVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _newspaperService.CreateNewspaperAsync(viewModel.Newspaper);
                    TempData["success"] = "Газета успешно добавлена";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при сохранении данных. Нарушена связь с редактором.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Произошла неизвестная ошибка: " + ex.Message);
                }
            }
            viewModel.Editors = await _selectListService.GetEditorsSelectListAsync();
            return View("Upsert",viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int newspaperId)
        {
            var newspaper = await _newspaperService.GetNewspaperByIdAsync(newspaperId);
            if (newspaper == null)
            {
                return NotFound();
            }
            var viewModel = new NewspaperVM
            {
                Editors = await _selectListService.GetEditorsSelectListAsync(),
                Newspaper = newspaper
            };
            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(NewspaperVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _newspaperService.UpdateNewspaperAsync(viewModel.Newspaper);
                    TempData["success"] = "Газета успешно обновлена";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError(string.Empty, "Ошибка при обновлении данных. Нарушена связь с редактором.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Произошла неизвестная ошибка: " + ex.Message);
                }
            }
            viewModel.Editors = await _selectListService.GetEditorsSelectListAsync();
            return View("Upsert",viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int newspaperId)
        {
            var newspaper = await _newspaperService.GetNewspaperByIdAsync(newspaperId);

            if (newspaper == null)
            {
                return NotFound();
            }

            return View(newspaper);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int newspaperId)
        {
            await _newspaperService.DeleteNewspaperAsync(newspaperId);

            TempData["success"] = "Газета успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
