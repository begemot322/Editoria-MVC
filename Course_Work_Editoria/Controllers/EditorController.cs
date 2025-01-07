using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Editoria.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers
{
    public class EditorController : Controller
    {
        private readonly IEditorRepository _editorRepository;

        public EditorController(IEditorRepository editorRepository)
        {
            _editorRepository = editorRepository;
        }

        public IActionResult Index(string name, string email)
        {
            var editors = _editorRepository.GetFilteredEditors(name, email);

            EditorListViewModel viewModel = new EditorListViewModel
            {
                Editors = editors.ToList(),
                Name = name,
                Email = email
            };

            return View(viewModel);
        }

        public IActionResult Upsert(int? editorId)
        {
            var editor = editorId.HasValue ?
                _editorRepository.GetEditorById(editorId.Value) : new Editor();

            if(editorId.HasValue && editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        [HttpPost]
        public IActionResult Upsert(Editor editor)
        {
            if (ModelState.IsValid)
            {
                if (editor.EditorId == 0)
                {
                    _editorRepository.AddEditor(editor);
                    TempData["success"] = "Редактор успешно добавлен!";
                }
                else
                {
                    _editorRepository.UpdateEditor(editor);
                    TempData["success"] = "Редактор успешно обновлён!";
                }
                return RedirectToAction("Index");
            }
            return View(editor);
        }


        public IActionResult Delete(int editorId)
        {
            var editor = _editorRepository.GetEditorById(editorId);

            if (editor == null)
            {
                return NotFound();
            }
            return View(editor);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int editorId)
        {
            _editorRepository.DeleteEditor(editorId);

            TempData["success"] = "Редактор успешно удалён";
            return RedirectToAction("Index");
        }

    }
}
