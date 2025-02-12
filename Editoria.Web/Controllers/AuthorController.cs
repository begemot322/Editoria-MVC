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
        public IActionResult Create()
        {
            return View("Upsert", new Author());
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.CreateAuthorAsync(author);
                TempData["success"] = "Автор успешно создан";
                return RedirectToAction("Index");
            }
            return View("Upsert", author);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int authorId)
        {
            var author = await _authorService.GetAuthorByIdAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            return View("Upsert", author);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(Author author)
        {
            if (ModelState.IsValid)
            {
                await _authorService.UpdateAuthorAsync(author);
                TempData["success"] = "Автор успешно обновлён";
                return RedirectToAction("Index");
            }
            return View("Upsert",author);
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
