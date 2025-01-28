using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddArticle(Article article, List<int> tagIds)
        {
            _db.Articles.Add(article);
            _db.SaveChanges();

            var articleTag = tagIds.Select(tagId => new ArticleTag
            {
                ArticleId = article.ArticleId,
                TagId = tagId
            }).ToList();

            _db.ArticleTags.AddRange(articleTag);
            _db.SaveChanges();
        }

        public void DeleteArticle(int articleId)
        {
            var article = _db.Articles.FirstOrDefault(a => a.ArticleId == articleId);
            if (article != null)
            {
                _db.Articles.Remove(article);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _db.Articles
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Author)
                .Include(a => a.ArticleTags) 
                    .ThenInclude(at => at.Tag) 
                .ToList();
        }

        public Article GetArticleById(int articleId)
        {
            var article = _db.Articles
                .Include(a => a.Issue)
                .Include(a => a.Category)
                .Include(a => a.Author)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .FirstOrDefault(a => a.ArticleId == articleId);

            return article;

        }

        public List<SelectListItem> GetAuthorSelectList()
        {
            return _db.Authors.Select(a => new SelectListItem
            {
                Text = $"{a.Name} {a.Surname}",
                Value = a.AuthorId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetCategorySelectList()
        {
            return _db.Categories
                .Where(c=>c.IsActive)
                .Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.CategoryId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetIssueSelectList()
        {
            return _db.Issues
                .Where(i=>!i.IsActive)
                .Select(i => new SelectListItem
            {
                Text = $"Номер выпуска: {i.IssueId.ToString()} - {i.Information}",
                Value = i.IssueId.ToString()
            }).ToList();
        }

        public List<SelectListItem> GetTagSelectList()
        {
            return _db.Tags.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.TagId.ToString()
            }).ToList();
        }

        public void UpdateArticle(Article article, List<int> tagIds)
        {
            _db.Articles.Update(article);
            _db.SaveChanges();

            var existingTags = _db.ArticleTags.Where(at => at.ArticleId == article.ArticleId).ToList();
            _db.ArticleTags.RemoveRange(existingTags);

            var newTags = tagIds.Select(tagId => new ArticleTag
            {
                ArticleId = article.ArticleId,
                TagId = tagId
            }).ToList();

            _db.ArticleTags.AddRange(newTags);
            _db.SaveChanges(); 
        }
    }
}
