using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IArticleRepository
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int articleId);
        void AddArticle(Article article, List<int> tagIds);
        void UpdateArticle(Article article, List<int> tagIds);
        void DeleteArticle(int articleId);
        List<SelectListItem> GetIssueSelectList();
        List<SelectListItem> GetCategorySelectList();
        List<SelectListItem> GetAuthorSelectList();
        List<SelectListItem> GetTagSelectList();
    }
}
