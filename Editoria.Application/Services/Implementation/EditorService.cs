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
    public class EditorService : IEditorService
    {
        private readonly IEditorRepository _editorRepository;
        public EditorService(IEditorRepository editorRepository)
        {
            _editorRepository = editorRepository;
        }

        public async Task CreateEditorAsync(Editor editor)
        {
            ArgumentNullException.ThrowIfNull(editor);

            await _editorRepository.AddAsync(editor);
        }

        public async Task<bool> DeleteEditorAsync(int editorId)
        {
            try
            {
                var editor = await _editorRepository.GetAsync(a => a.EditorId == editorId);

                if (editor != null)
                {
                    await _editorRepository.DeleteAsync(editor);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Editor with ID {editorId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<IEnumerable<Editor>> GetAllEditorsAsync(string nameFilter, string emailFilter)
        {
            if (!string.IsNullOrEmpty(nameFilter) && !string.IsNullOrEmpty(emailFilter))
            {
                return await _editorRepository.GetAllAsync(
                    u => u.Name.Contains(nameFilter) && u.Email.Contains(emailFilter));
            }
            else
            {
                if (!string.IsNullOrEmpty(nameFilter))
                {
                    return await _editorRepository.GetAllAsync(
                        u => u.Name.Contains(nameFilter));
                }
                if (!string.IsNullOrEmpty(emailFilter))
                {
                    return await _editorRepository.GetAllAsync(
                        u => u.Email.Contains(emailFilter));
                }
            }
            return await _editorRepository.GetAllAsync();
        }

        public async Task<Editor?> GetEditorByIdAsync(int id)
        {
            return await _editorRepository.GetAsync(u => u.EditorId == id);
        }

        public async Task UpdateEditorAsync(Editor editor)
        {
            ArgumentNullException.ThrowIfNull(editor);

            await _editorRepository.UpdateAsync(editor);
        }

    }
}
