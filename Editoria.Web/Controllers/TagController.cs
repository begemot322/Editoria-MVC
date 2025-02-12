using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Editoria.Web.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var tagList = await _tagService.GetAllTagsAsync();
            return View(tagList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Create()
        {
            return View("Upsert", new Tag());
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.CreateTagAsync(tag);
                TempData["success"] = "Тег создан успешно!";
                return RedirectToAction("Index");
            }
            return View("Upsert", tag);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int tagId)
        {
            var tag = await _tagService.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                return NotFound();
            }
            return View("Upsert", tag);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _tagService.UpdateTagAsync(tag);
                TempData["success"] = "Тег обновлён успешно!";
                return RedirectToAction("Index");
            }
            return View("Upsert", tag);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int tagId)
        {
            var tag = await _tagService.GetTagByIdAsync(tagId);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int tagId)
        {
            await _tagService.DeleteTagAsync(tagId);

            TempData["success"] = "Тег удалён успешно!";
            return RedirectToAction("Index");
        }
    }
}
