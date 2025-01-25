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
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;
        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddTag(Tag tag)
        {
            _db.Tags.Add(tag);
            _db.SaveChanges();
        }

        public void DeleteTag(int tagId)
        {
            var tag = _db.Tags.FirstOrDefault(t => t.TagId == tagId);
            if (tag != null)
            {
                _db.Tags.Remove(tag);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Tag> GetAllTags()
        {
            return _db.Tags.ToList();
        }

        public Tag GetTagById(int tagId)
        {
            return _db.Tags.FirstOrDefault(t => t.TagId == tagId);
        }

        public void UpdateTag(Tag tag)
        {
            _db.Tags.Update(tag);
            _db.SaveChanges();
        }
    }
}
