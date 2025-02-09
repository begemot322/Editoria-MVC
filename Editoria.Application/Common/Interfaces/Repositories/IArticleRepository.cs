using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Common.Interfaces.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task AddArticleTagsAsync(List<ArticleTag> articleTags);
        Task<Article?> GetArticleByIdWithThenIncludesAsync(int articleId);
        Task<IEnumerable<Article>> GetArticlesWithThenIncludesAsync();
        Task<List<ArticleTag>> GetArticleTagsAsync(int articleId);
        Task RemoveArticleTagsAsync(List<ArticleTag> articleTags);
    }
}
