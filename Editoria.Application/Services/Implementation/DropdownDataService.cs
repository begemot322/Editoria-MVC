using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Domain.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class DropdownDataService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly INewspaperRepository _newspaperRepository;
        private readonly IEditorRepository _editorRepository;

        public DropdownDataService(
            IIssueRepository issueRepository,
            IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            INewspaperRepository newspaperRepository,
            IEditorRepository editorRepository)
        {
            _issueRepository = issueRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _newspaperRepository = newspaperRepository;
            _editorRepository = editorRepository;
        }

        public async Task<List<SelectListItem>> GetIssueSelectListAsync()
        {
            var issues = await _issueRepository.GetAllAsync(i => i.IsActive);

            return issues.Select(n => new SelectListItem
            {
                Text = $"Номер выпуска: {n.IssueId} - {n.Information}",
                Value = n.IssueId.ToString()
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetAuthorSelectListAsync()
        {
            var authors = await _authorRepository.GetAllAsync();

            return authors.Select(a => new SelectListItem
            {
                Text = $"{a.Name} {a.Surname}",
                Value = a.AuthorId.ToString()
            }).ToList();
        }
        public async Task<List<SelectListItem>> GetCategorySelectListAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(c => c.IsActive);

            return categories.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();
        }
        public async Task<List<SelectListItem>> GetTagSelectListAsync()
        {
            var tags = await _tagRepository.GetAllAsync();

            return tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.TagId.ToString()
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetNewspaperSelectListAsync()
        {
            var newspapers = await _newspaperRepository.GetAllAsync();

            return newspapers.Select(n => new SelectListItem
            {
                Text = n.Name,
                Value = n.NewspaperId.ToString()
            }).ToList();
        }
        public async Task<List<SelectListItem>> GetEditorsSelectListAsync()
        {
            var editors = await _editorRepository.GetAllAsync();
            return editors.Select(e => new SelectListItem
            {
                Text = $"{e.Name} {e.Surname}",
                Value = e.EditorId.ToString()
            }).ToList();
        }
        public List<SelectListItem> GetRolesSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Admin", Value = Roles.Admin },
                new SelectListItem { Text = "User", Value = Roles.User },
                new SelectListItem { Text = "Moderator", Value = Roles.Moderator }
            };
        }
    }
}
