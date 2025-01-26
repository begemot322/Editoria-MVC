﻿using Editoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Editoria.Models.ViewModel;
using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Course_Work_Editoria.Controllers
{
    public class IssueController : Controller
    {
        private readonly IIssueRepository _issueRepository;

        public IssueController(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }

        [Authorize(Policy = "UserPolicy")]
        public IActionResult Index()
        {
            var issueList = _issueRepository.GetAllIssues();
            return View(issueList);
        }

        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(int? issueId)
        {
            var viewModel = new IssueVM
            {
                Newspapers = _issueRepository.GetNewspaperList(),
                Issue = issueId.HasValue ?
                    _issueRepository.GetIssueById(issueId.Value) : new Issue() 
            };

            if (issueId.HasValue && viewModel.Issue == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorPolicy")]
        public IActionResult Upsert(IssueVM viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.Issue.IssueId == 0)
                {
                    _issueRepository.AddIssue(viewModel.Issue);
                    TempData["success"] = "Выпуск успешно добавлен";
                }
                else
                {
                    _issueRepository.UpdateIssue(viewModel.Issue);
                    TempData["success"] = "Выпуск успешно обновлён";
                }
                return RedirectToAction("Index");
            }

            viewModel.Newspapers = _issueRepository.GetNewspaperList();
            return View(viewModel);
        }

        [Authorize(Policy = "AdminPolicy")]
        public IActionResult Delete(int issueId)
        {
            var issue = _issueRepository.GetIssueById(issueId);

            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminPolicy")]
        public IActionResult DeletePOST(int issueId)
        {
            _issueRepository.DeleteIssue(issueId);

            TempData["success"] = "Выпуск успешно удалён";
            return RedirectToAction("Index");
        }
    }
}
