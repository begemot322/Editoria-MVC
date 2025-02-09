using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface IEditorService
    {
        Task CreateEditorAsync(Editor editor);
        Task<bool> DeleteEditorAsync(int editorId);
        Task<IEnumerable<Editor>> GetAllEditorsAsync(string nameFilter, string emailFilter);
        Task<Editor?> GetEditorByIdAsync(int id);
        Task UpdateEditorAsync(Editor editor);
    }
}