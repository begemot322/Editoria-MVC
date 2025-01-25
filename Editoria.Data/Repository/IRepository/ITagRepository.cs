using Editoria.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAllTags();
        Tag GetTagById(int tagId);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(int tagId);
    }
}
