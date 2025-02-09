using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Editoria.Web.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var authorList = await _authorService.GetAllAuthorsAsync();

            return View(authorList);
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Details(int AuthorId)
        {
            var author = await _authorService.GetAuthorByIdAsync(AuthorId);

            return View(author);
        }


        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(int? authorId)
        {
            var author = authorId.HasValue
                ? await _authorService.GetAuthorByIdAsync(authorId.Value)
                : new Author();

            if (authorId.HasValue && author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0)
                {
                    await _authorService.CreateAuthorAsync(author);
                    TempData["success"] = "Автор успешно создан";
                }
                else
                {
                    await _authorService.UpdateAuthorAsync(author);
                    TempData["success"] = "Автор успешно обновлён";
                }
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int AuthorId)
        {
            var author = await _authorService.GetAuthorByIdAsync(AuthorId);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int AuthorId)
        {
            await _authorService.DeleteAuthorAsync(AuthorId);

            TempData["success"] = "Автор успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
