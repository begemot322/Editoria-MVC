using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface ITagService
    {
        Task CreateTagAsync(Tag tag);
        Task<bool> DeleteTagAsync(int tagId);
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag?> GetTagByIdAsync(int id);
        Task UpdateTagAsync(Tag tag);
    }
}