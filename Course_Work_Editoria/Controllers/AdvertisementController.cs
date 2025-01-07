using Editoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editoria.Models.ViewModel;
using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using NuGet.Protocol.Core.Types;

namespace Course_Work_Editoria.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public AdvertisementController(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public IActionResult Index(string typeFilter, int? issueFilter)
        {
            var advertisementList = _advertisementRepository.GetFilteredAdvertisements(typeFilter, issueFilter);
            var issueSelectList = _advertisementRepository.GetIssueSelectList();

            var viewModel = new AdvertisementFilterVM
            {
                Advertisements = advertisementList.ToList(),
                TypeFilter = typeFilter,
                IssueFilter = issueFilter,
                IssueSelectList = issueSelectList
            };
            return View(viewModel);
        }

        public IActionResult Upsert(int? advertisementId)
        {
            var viewModel = new AdvertisementVM()
            {
                Issues = _advertisementRepository.GetIssueSelectList(),
                Advertisement = advertisementId.HasValue?
                _advertisementRepository.GetAdvertisementById(advertisementId): new Advertisement()
            };

            if (advertisementId.HasValue && viewModel.Advertisement == null)
            {
                return NotFound();
            }
            return View(viewModel);

        }

        [HttpPost]
        public IActionResult Upsert(AdvertisementVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Advertisement.AdvertisementId == 0)
                {
                    _advertisementRepository.AddAdvertisement(viewModel.Advertisement);
                    TempData["success"] = "Рекламное объявление успешно добавлено";
                }
                else
                {
                    _advertisementRepository.UpdateAdvertisement(viewModel.Advertisement);
                    TempData["success"] = "Рекламное объявление успешно обновлено";
                }
                return RedirectToAction("Index");
            }
            viewModel.Issues = _advertisementRepository.GetIssueSelectList();
            return View(viewModel);
        }

        public IActionResult Delete(int advertisementId)
        {
           var advertisement = _advertisementRepository.GetAdvertisementWithIssue(advertisementId);
           if (advertisement == null)
           {
                return NotFound();
           }
           return View(advertisement);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int advertisementId)
        {
            _advertisementRepository.DeleteAdvertisement(advertisementId);

            TempData["success"] = "Рекламное объявление успешно удалено";
            return RedirectToAction("Index");
        }
    }
}
