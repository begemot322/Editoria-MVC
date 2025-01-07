using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class IssueRepository : IIssueRepository
    {
        private readonly ApplicationDbContext _db;
        public IssueRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddIssue(Issue issue)
        {
            _db.Issues.Add(issue);
            _db.SaveChanges();
        }

        public void DeleteIssue(int issueId)
        {
            var issue = _db.Issues.FirstOrDefault(i => i.IssueId == issueId);
            if (issue != null)
            {
                _db.Issues.Remove(issue);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Issue> GetAllIssues()
        {
            return _db.Issues.Include(i => i.Newspaper).ToList();
        }

        public Issue GetIssueById(int issueId)
        {
            return _db.Issues.Include(i => i.Newspaper).FirstOrDefault(i => i.IssueId == issueId);
        }

        public List<SelectListItem> GetNewspaperList()
        {
            return _db.Newspapers.Select(n => new SelectListItem
            {
                Text = n.Name,
                Value = n.NewspaperId.ToString()
            }).ToList();
        }

        public void UpdateIssue(Issue issue)
        {
            _db.Issues.Update(issue);
            _db.SaveChanges();
        }
    }
}
