using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task CreateArticleAsync(Article article, List<int> tagIds)
        {
            ArgumentNullException.ThrowIfNull(article);

            await _articleRepository.AddAsync(article);

            var articleTags = tagIds.Select(tagId => new ArticleTag
            {
                ArticleId = article.ArticleId,
                TagId = tagId
            }).ToList();

            await _articleRepository.AddArticleTagsAsync(articleTags);
        }

        public async Task UpdateArticleAsync(Article article, List<int> tagIds)
        {
            ArgumentNullException.ThrowIfNull(article);

            await _articleRepository.UpdateAsync(article);

            var existingTags = await _articleRepository.GetArticleTagsAsync(article.ArticleId);
            await _articleRepository.RemoveArticleTagsAsync(existingTags);

            var newTags = tagIds.Select(tagId => new ArticleTag
            {
                ArticleId = article.ArticleId,
                TagId = tagId
            }).ToList();

            await _articleRepository.AddArticleTagsAsync(newTags);
        }

        public async Task<bool> DeleteArticleAsync(int articleId)
        {
            try
            {
                var article = await _articleRepository.GetAsync(a => a.ArticleId == articleId, tracked: true);

                if (article != null)
                {
                    await _articleRepository.DeleteAsync(article);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Advertisement with ID {articleId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<Article?> GetArticleByIdAsync(int articleId)
        {
            return await _articleRepository.GetArticleByIdWithThenIncludesAsync(articleId);

        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _articleRepository.GetArticlesWithThenIncludesAsync();
        }

    }
}
