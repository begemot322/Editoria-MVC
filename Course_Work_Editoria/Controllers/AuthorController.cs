using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers
{
    public class AuthorController : Controller
    {

        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            var authorList = _authorRepository.GetAllAuthors();
            return View(authorList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(int? authorId)
        {
            var author = authorId.HasValue?
                _authorRepository.GetAuthorById(authorId.Value) : new Author();

            if (authorId.HasValue && author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.AuthorId == 0)
                {
                    _authorRepository.AddAuthor(author);
                    TempData["success"] = "Автор успешно создан";
                }
                else
                {
                    _authorRepository.UpdateAuthor(author);
                    TempData["success"] = "Автор успешно обновлён";
                }
                return RedirectToAction("Index");
            }
            return View(author);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int AuthorId)
        {
           var author = _authorRepository.GetAuthorById(AuthorId);

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeletePOST(int AuthorId)
        {
            _authorRepository.DeleteAuthor(AuthorId);

            TempData["success"] = "Автор успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
