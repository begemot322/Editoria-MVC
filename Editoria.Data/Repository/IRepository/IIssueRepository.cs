using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IIssueRepository
    {
        IEnumerable<Issue> GetAllIssues();
        Issue GetIssueById(int issueId);
        void AddIssue(Issue issue);
        void UpdateIssue(Issue issue);
        void DeleteIssue(int issueId);
        List<SelectListItem> GetNewspaperList();
    }
}
