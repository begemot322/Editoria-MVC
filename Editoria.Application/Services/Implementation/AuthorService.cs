using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorService(IAuthorRepository athorRepository)
        {
            _authorRepository = athorRepository;
        }

        public async Task CreateAuthorAsync(Author author)
        {
            ArgumentNullException.ThrowIfNull(author);

            await _authorRepository.AddAsync(author);
        }
        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetAsync(a => a.AuthorId == authorId);

                if (author != null)
                {
                    await _authorRepository.DeleteAsync(author);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Author with ID {authorId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _authorRepository.GetAsync(a => a.AuthorId == id);
        }

        public async Task UpdateAuthorAsync(Author author)
        {
            ArgumentNullException.ThrowIfNull(author);

            await _authorRepository.UpdateAsync(author);
        }

    }
}
