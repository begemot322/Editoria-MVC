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
        public async Task<IActionResult> Upsert(int? issueId)
        {
            var viewModel = new IssueVM
            {
                Newspapers = await _dropdownService.GetNewspaperSelectListAsync(),
                Issue = issueId.HasValue ?
                    await _issueService.GetIssueByIdAsync(issueId.Value) : new Issue()
            };

            if (issueId.HasValue && viewModel.Issue == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public async Task<IActionResult> Upsert(IssueVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Issue.IssueId == 0)
                {
                    await _issueService.CreateIssueAsync(viewModel.Issue);
                    TempData["success"] = "Выпуск успешно добавлен";
                }
                else
                {
                    await _issueService.UpdateIssueAsync(viewModel.Issue);
                    TempData["success"] = "Выпуск успешно обновлён";
                }
                return RedirectToAction("Index");
            }

            viewModel.Newspapers = await _dropdownService.GetNewspaperSelectListAsync();
            return View(viewModel);
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
