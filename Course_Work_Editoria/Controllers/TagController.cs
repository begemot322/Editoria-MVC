﻿using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Course_Work_Editoria.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            var tagList = _tagRepository.GetAllTags();
            return View(tagList);
        }

        public IActionResult Upsert(int? tagId)
        {
            var tag = tagId.HasValue ?
                _tagRepository.GetTagById(tagId.Value) : new Tag();

            if (tagId.HasValue && tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost]
        public IActionResult Upsert(Tag tag)
        {
            if (ModelState.IsValid)
            {
                if (tag.TagId == 0)
                {
                    _tagRepository.AddTag(tag);
                    TempData["success"] = "Тег создан успешно!";
                }
                else
                {
                    _tagRepository.UpdateTag(tag);
                    TempData["success"] = "Тег обновлён успешно!";
                }
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        public IActionResult Delete(int tagId)
        {
            var tag = _tagRepository.GetTagById(tagId);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int tagId)
        {
            _tagRepository.DeleteTag(tagId);

            TempData["success"] = "Тег удалён успешно!";
            return RedirectToAction("Index");
        }
    }
}
