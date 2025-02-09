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
        public async Task<IActionResult> Upsert(int? editorId)
        {
            var editor = editorId.HasValue ?
                await _editorService.GetEditorByIdAsync(editorId.Value) : new Editor();

            if (editorId.HasValue && editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(Editor editor)
        {
            if (ModelState.IsValid)
            {
                if (editor.EditorId == 0)
                {
                    await _editorService.CreateEditorAsync(editor);
                    TempData["success"] = "Редактор успешно добавлен!";
                }
                else
                {
                    await _editorService.UpdateEditorAsync(editor);
                    TempData["success"] = "Редактор успешно обновлён!";
                }
                return RedirectToAction("Index");
            }
            return View(editor);
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
