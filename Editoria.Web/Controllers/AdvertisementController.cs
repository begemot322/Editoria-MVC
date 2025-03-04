using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Editoria.Domain.Entities;
using Editoria.Application.Services.Services;
using Editoria.Application.Services.Implementation;
using Editoria.Web.ViewModel;
using Editoria.Web.Services;

namespace Editoria.Web.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly DropdownDataService _dropdownService;

        public AdvertisementController(
            IAdvertisementService advertisementService,
            DropdownDataService dropdownService)
        {
            _advertisementService = advertisementService;
            _dropdownService = dropdownService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index(string typeFilter, int? issueFilter)
        {
            var advertisementList = await _advertisementService.GetAllAdvertisementsAsync(typeFilter, issueFilter);
            var issueSelectList = await _dropdownService.GetIssueSelectListAsync();

            var viewModel = new AdvertisementFilterVM
            {
                Advertisements = advertisementList,
                TypeFilter = typeFilter,
                IssueFilter = issueFilter,
                IssueSelectList = issueSelectList
            };
            return View(viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new AdvertisementVM
            {
                Issues = await _dropdownService.GetIssueSelectListAsync(),
                Advertisement = new Advertisement()
            };

            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(AdvertisementVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _advertisementService.CreateAdvertisementAsync(viewModel.Advertisement);
                TempData["success"] = "Рекламное объявление успешно добавлено";
                return RedirectToAction("Index");
            }

            viewModel.Issues = await _dropdownService.GetIssueSelectListAsync();
            return View("Upsert", viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int advertisementId)
        {
            var advertisement = await _advertisementService.GetAdvertisementByIdAsync(advertisementId);
            if (advertisement == null) return NotFound();

            var viewModel = new AdvertisementVM
            {
                Issues = await _dropdownService.GetIssueSelectListAsync(),
                Advertisement = advertisement
            };

            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(AdvertisementVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _advertisementService.UpdateAdvertisementAsync(viewModel.Advertisement);
                TempData["success"] = "Рекламное объявление успешно обновлено";
                return RedirectToAction("Index");
            }

            viewModel.Issues = await _dropdownService.GetIssueSelectListAsync();
            return View("Upsert",viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int advertisementId)
        {
            var advertisement = await _advertisementService.GetAdvertisementByIdAsync(advertisementId);
            if (advertisement == null)
            {
                return NotFound();
            }
            return View(advertisement);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int advertisementId)
        {
            await _advertisementService.DeleteAdvertisementAsync(advertisementId);

            TempData["success"] = "Рекламное объявление успешно удалено";
            return RedirectToAction("Index");
        }
    }
}
