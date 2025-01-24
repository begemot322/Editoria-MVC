using Editoria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IEditorRepository
    {
        IEnumerable<Editor> GetFilteredEditors(string nameFilter, string emailFilter);
        Editor GetEditorById(int editorId);
        void AddEditor(Editor editor);
        void UpdateEditor(Editor editor);
        void DeleteEditor(int editorId);
    }
}
