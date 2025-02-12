using Editoria.Application.Services.Implementation;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Editoria.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Editoria.Web.Controllers
{
    public class EditorController : Controller
    {
        private readonly IEditorService _editorService;

        public EditorController(IEditorService editorService)
        {
            _editorService = editorService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index(string name, string email)
        {
            var editors = await _editorService.GetAllEditorsAsync(name, email);

            EditorFilterVM viewModel = new EditorFilterVM
            {
                Editors = editors,
                Name = name,
                Email = email
            };

            return View(viewModel);
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Details(int editorId)
        {
            var editor = await _editorService.GetEditorByIdAsync(editorId);

            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Create()
        {
            return View("Upsert", new Editor());
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(Editor editor)
        {
            if (ModelState.IsValid)
            {
                await _editorService.CreateEditorAsync(editor);
                TempData["success"] = "Редактор добавлен успешно!";
                return RedirectToAction("Index");
            }
            return View("Upsert", editor);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int editorId)
        {
            var editor = await _editorService.GetEditorByIdAsync(editorId);
            if (editor == null)
            {
                return NotFound();
            }
            return View("Upsert", editor);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(Editor editor)
        {
            if (ModelState.IsValid)
            {
                await _editorService.UpdateEditorAsync(editor);
                TempData["success"] = "Редактор успешно обновлён!";
                return RedirectToAction("Index");
            }
            return View("Upsert", editor);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int editorId)
        {
            var editor = await _editorService.GetEditorByIdAsync(editorId);

            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int editorId)
        {
            await _editorService.DeleteEditorAsync(editorId);

            TempData["success"] = "Редактор успешно удалён";
            return RedirectToAction("Index");
        }

    }
}
