using Editoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAllAuthors();
        Author GetAuthorById(int authorId);
        void AddAuthor(Author author);
        void UpdateAuthor(Author author);
        void DeleteAuthor(int authorId);
    }
}
