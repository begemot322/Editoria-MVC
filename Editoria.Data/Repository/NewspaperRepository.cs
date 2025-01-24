using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class NewspaperRepository : INewspaperRepository
    {
        private readonly ApplicationDbContext _db;
        public NewspaperRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddNewspaper(Newspaper newspaper)
        {
            _db.Newspapers.Add(newspaper);
            _db.SaveChanges();
        }

        public void DeleteNewspaper(int newspaperId)
        {
            var newspaper = _db.Newspapers.FirstOrDefault(n => n.NewspaperId == newspaperId);
            if (newspaper != null)
            {
                _db.Newspapers.Remove(newspaper);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Newspaper> GetAllNewspapers()
        {
            return _db.Newspapers.Include(n => n.Editor).ToList();
        }

        public List<SelectListItem> GetEditorsList()
        {
            return _db.Editors.Select(e => new SelectListItem
            {
                Text = $"{e.Name} {e.Surname}",
                Value = e.EditorId.ToString()
            }).ToList();
        }

        public Newspaper GetNewspaperById(int newspaperId)
        {
            return _db.Newspapers.Include(n => n.Editor).FirstOrDefault(n => n.NewspaperId == newspaperId);
        }

        public void UpdateNewspaper(Newspaper newspaper)
        {
            _db.Newspapers.Update(newspaper);
            _db.SaveChanges();
        }
    }
}
