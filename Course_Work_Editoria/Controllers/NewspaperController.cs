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
    public class NewspaperController : Controller
    {
        private readonly INewspaperRepository _newspaperRepository;
        public NewspaperController(INewspaperRepository newspaperRepository)
        {
            _newspaperRepository = newspaperRepository;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            var newspaperList = _newspaperRepository.GetAllNewspapers();
            return View(newspaperList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(int? newspaperId)
        {
            var viewModel = new NewspaperVM
            {
                Editors = _newspaperRepository.GetEditorsList(),
                Newspaper = newspaperId.HasValue ? 
                    _newspaperRepository.GetNewspaperById(newspaperId.Value) : new Newspaper()
            };

            if(newspaperId.HasValue && viewModel.Newspaper == null)
            {
                return NotFound();
            }
            return View(viewModel); 
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(NewspaperVM viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.Newspaper.NewspaperId == 0)
                    {
                        _newspaperRepository.AddNewspaper(viewModel.Newspaper);
                        TempData["success"] = "Газета успешно добавлена";
                    }
                    else
                    {
                        _newspaperRepository.UpdateNewspaper(viewModel.Newspaper);
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

            viewModel.Editors = _newspaperRepository.GetEditorsList();
            return View(viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int newspaperId)
        {
            var newspaper = _newspaperRepository.GetNewspaperById(newspaperId);

            if (newspaper == null)
            {
                return NotFound();
            }

            return View(newspaper);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeletePOST(int newspaperId)
        {
            _newspaperRepository.DeleteNewspaper(newspaperId);

            TempData["success"] = "Газета успешно удалена";
            return RedirectToAction("Index");
        }
    }
}
