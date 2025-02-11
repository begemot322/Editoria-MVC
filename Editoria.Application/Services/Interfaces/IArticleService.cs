using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface IArticleService
    {
        Task CreateArticleAsync(Article article, List<int> tagIds);
        Task<bool> DeleteArticleAsync(int articleId);
        Task<IEnumerable<Article>> GetAllArticlesAsync();
        Task<Article?> GetArticleByIdAsync(int articleId);
        Task UpdateArticleAsync(Article article, List<int> tagIds);
        Task<List<Article>> SearchArticlesAsync(string keyword);

    }
}