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
    public class NewspaperService : INewspaperService
    {
        private readonly INewspaperRepository _newspaperRepository;
        public NewspaperService(INewspaperRepository newspaperRepository)
        {
            _newspaperRepository = newspaperRepository;
        }
        public async Task<IEnumerable<Newspaper>> GetAllNewspapersAsync()
        {
            return await _newspaperRepository.GetAllAsync(includeProperties: "Editor");
        }
        public async Task CreateNewspaperAsync(Newspaper newspaper)
        {
            ArgumentNullException.ThrowIfNull(newspaper);

            await _newspaperRepository.AddAsync(newspaper);
        }
        public async Task<bool> DeleteNewspaperAsync(int newspaperId)
        {
            try
            {
                var newspaper = await _newspaperRepository.GetAsync(i => i.NewspaperId == newspaperId);

                if (newspaper != null)
                {
                    await _newspaperRepository.DeleteAsync(newspaper);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Newspaper with ID {newspaperId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        public async Task<Newspaper?> GetNewspaperByIdAsync(int id)
        {
            return await _newspaperRepository.GetAsync(a => a.NewspaperId == id, includeProperties: "Editor");
        }
        public async Task UpdateNewspaperAsync(Newspaper newspaper)
        {
            ArgumentNullException.ThrowIfNull(newspaper);

            await _newspaperRepository.UpdateAsync(newspaper);
        }

    }
}
