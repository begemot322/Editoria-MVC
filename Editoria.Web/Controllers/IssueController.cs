using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Editoria.Domain.Entities;
using Editoria.Application.Services.Services;
using Editoria.Application.Services.Implementation;
using Editoria.Web.ViewModel;

namespace Editoria.Web.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssueService _issueService;
        private readonly DropdownDataService _dropdownService;

        public IssueController(IIssueService issueService,
            DropdownDataService dropdownService)
        {
            _issueService = issueService;
            _dropdownService = dropdownService;
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Index()
        {
            var issueList = await _issueService.GetAllIssuesAsync();
            return View(issueList);
        }

        [Authorize(Policy = "UserPolicy")]
        public async Task<IActionResult> Details(int? issueId)
        {
            var viewModel = new IssueVM
            {
                Issue = issueId.HasValue ?
                    await _issueService.GetIssueByIdAsync(issueId.Value) : new Issue(),
                TotalCost = await _issueService.GetTotalCostAsync(issueId.Value)
            };
            return View(viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create()
        {
            var viewModel = new IssueVM
            {
                Newspapers = await _dropdownService.GetNewspaperSelectListAsync(),
                Issue = new Issue()
            };
            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Create(IssueVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _issueService.CreateIssueAsync(viewModel.Issue);
                TempData["success"] = "Выпуск успешно добавлен";
                return RedirectToAction("Index");
            }

            viewModel.Newspapers = await _dropdownService.GetNewspaperSelectListAsync();
            return View("Upsert", viewModel);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(int issueId)
        {
            var issue = await _issueService.GetIssueByIdAsync(issueId);
            if (issue == null)
            {
                return NotFound();
            }

            var viewModel = new IssueVM
            {
                Newspapers = await _dropdownService.GetNewspaperSelectListAsync(),
                Issue = issue
            };
            return View("Upsert", viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Update(IssueVM viewModel)
        {
            if (ModelState.IsValid)
            {
                await _issueService.UpdateIssueAsync(viewModel.Issue);
                TempData["success"] = "Выпуск успешно обновлён";
                return RedirectToAction("Index");
            }

            viewModel.Newspapers = await _dropdownService.GetNewspaperSelectListAsync();
            return View("Upsert", viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(int issueId)
        {
            var issue = await _issueService.GetIssueByIdAsync(issueId);

            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeletePOST(int issueId)
        {
            await _issueService.DeleteIssueAsync(issueId);

            TempData["success"] = "Выпуск успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
