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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public AuthorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddAuthor(Author author)
        {
            _db.Authors.Add(author);
            _db.SaveChanges();
        }

        public void DeleteAuthor(int authorId)
        {
            var author = GetAuthorById(authorId);
            if (author != null)
            {
                _db.Authors.Remove(author);
                _db.SaveChanges();
            }
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _db.Authors.ToList();
        }

        public Author GetAuthorById(int authorId)
        {
            return _db.Authors.FirstOrDefault(a => a.AuthorId == authorId);
        }

        public void UpdateAuthor(Author author)
        {
            _db.Authors.Update(author);
            _db.SaveChanges();
        }
    }
}
