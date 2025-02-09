using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface IAuthorService
    {
        Task CreateAuthorAsync(Author author);
        Task<bool> DeleteAuthorAsync(int authorId);
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int id);
        Task UpdateAuthorAsync(Author author);
    }
}