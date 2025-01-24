using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class EditorRepository : IEditorRepository
    {
        private readonly ApplicationDbContext _db;
        public EditorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddEditor(Editor editor)
        {
            _db.Editors.Add(editor);
            _db.SaveChanges();
        }

        public void DeleteEditor(int editorId)
        {
            var editor = _db.Editors.FirstOrDefault(e => e.EditorId == editorId);
            if (editor != null)
            {
                _db.Editors.Remove(editor);
                _db.SaveChanges();
            }
        }

        public Editor GetEditorById(int editorId)
        {
            return _db.Editors.FirstOrDefault(e => e.EditorId == editorId);
        }

        public IEnumerable<Editor> GetFilteredEditors(string nameFilter, string emailFilter)
        {
            var editors = _db.Editors.AsQueryable();

            if (!string.IsNullOrEmpty(nameFilter))
            {
                editors = editors.Where(e => e.Name.Contains(nameFilter));
            }

            if (!string.IsNullOrEmpty(emailFilter))
            {
                editors = editors.Where(e => e.Email.Contains(emailFilter));
            }

            return editors.ToList();
        }

        public void UpdateEditor(Editor editor)
        {
            _db.Editors.Update(editor);
            _db.SaveChanges();
        }
    }
}
