using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Domain.Entities;
using Editoria.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Infrastructure.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly ApplicationDbContext _db;
        public ArticleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task AddArticleTagsAsync(List<ArticleTag> articleTags)
        {
            await _db.ArticleTags.AddRangeAsync(articleTags);
            await _db.SaveChangesAsync();
        }

        public async Task<Article?> GetArticleByIdWithThenIncludesAsync(int articleId)
        {
            return await _db.Articles
                .Include(a => a.Issue)
                    .ThenInclude(i => i.Newspaper)
                        .ThenInclude(n => n.Editor)
                .Include(a => a.Category)
                .Include(a => a.Author)
                .Include(a => a.ArticleTags)
                    .ThenInclude(at => at.Tag)
                .FirstOrDefaultAsync(a => a.ArticleId == articleId);
        }

        public async Task<IEnumerable<Article>> GetArticlesWithThenIncludesAsync()
        {
            return await _db.Articles
                 .Include(a => a.Issue)
                     .ThenInclude(i => i.Newspaper)
                         .ThenInclude(n => n.Editor)
                 .Include(a => a.Category)
                 .Include(a => a.Author)
                 .Include(a => a.ArticleTags)
                     .ThenInclude(at => at.Tag)
                .ToListAsync();
        }

        public async Task<List<ArticleTag>> GetArticleTagsAsync(int articleId)
        {
            return await _db.ArticleTags.Where(at => at.ArticleId == articleId).ToListAsync();
        }

        public async Task RemoveArticleTagsAsync(List<ArticleTag> articleTags)
        {
            _db.ArticleTags.RemoveRange(articleTags);
            await _db.SaveChangesAsync();
        }
    }
}
