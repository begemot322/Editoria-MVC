using Course_Work_Editoria.Data;
using Course_Work_Editoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Course_Work_Editoria.Models.VIewModel;

namespace Course_Work_Editoria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdvertisementController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(string typeFilter, int? issueFilter)
        {
            var advertisementList = _db.Advertisements.Include(a => a.Issue).AsQueryable();

            if (!string.IsNullOrEmpty(typeFilter))
            {
                advertisementList = advertisementList.Where(a => a.Type.Contains(typeFilter));
            }
            if (issueFilter.HasValue)
            {
                advertisementList = advertisementList.Where(a => a.IssueId == issueFilter.Value);
            }

            var viewModel = new AdvertisementFilterVM
            {
                Advertisements = advertisementList.ToList(),
                TypeFilter = typeFilter,
                IssueFilter = issueFilter,
                IssueSelectList = _db.Issues.Select(n => new SelectListItem
                {
                    Text = $"Номер выпуска: {n.IssueId} - {n.PublicationDate.ToShortDateString()}",
                    Value = n.IssueId.ToString()
                }).ToList()
            };

            return View(viewModel);
        }


        public IActionResult Upsert(int? advertisementId)
        {
            var viewModel = new AdvertisementVM()
            {
                Issues = _db.Issues.ToList().Select(n => new SelectListItem
                {
                    Text = $"Номер выпуска: {n.IssueId} - {n.PublicationDate.ToShortDateString()}",
                    Value = n.IssueId.ToString()
                }),
                Advertisement = new Advertisement()
            };

            if (advertisementId == null || advertisementId == 0)
            {
                return View(viewModel);
            }
            else
            {
                viewModel.Advertisement = _db.Advertisements.FirstOrDefault(i => i.AdvertisementId == advertisementId);
                if (viewModel.Advertisement == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }

        }

        [HttpPost]
        public IActionResult Upsert(AdvertisementVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Advertisement.AdvertisementId == 0)
                {
                    _db.Advertisements.Add(viewModel.Advertisement);
                    TempData["success"] = "Рекламное объявление успешно добавлено";
                }
                else
                {
                    _db.Advertisements.Update(viewModel.Advertisement);
                    TempData["success"] = "Рекламное объявление успешно обновлено";
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            viewModel.Issues = _db.Issues.ToList().Select(n => new SelectListItem
            {
                Text = $"{n.IssueId} - {n.PublicationDate.ToShortDateString()}",
                Value = n.IssueId.ToString()
            });

            return View(viewModel);
        }

        public IActionResult Delete(int advertisementId)
        {
            var advertisementFromDb = _db.Advertisements
                .Include(a => a.Issue)
                .FirstOrDefault(a => a.AdvertisementId == advertisementId);
            if (advertisementFromDb == null)
            {
                return NotFound();
            }
            return View(advertisementFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int advertisementId)
        {
            var advertisement = _db.Advertisements
                .FirstOrDefault(a => a.AdvertisementId == advertisementId);
            if (advertisement == null)
            {
                return NotFound();
            }

            _db.Advertisements.Remove(advertisement);
            _db.SaveChanges();
            TempData["success"] = "Рекламное объявление успешно удалено";
            return RedirectToAction("Index");
        }
    }
}
