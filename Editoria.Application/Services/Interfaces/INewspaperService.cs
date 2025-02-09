using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface INewspaperService
    {
        Task CreateNewspaperAsync(Newspaper newspaper);
        Task<bool> DeleteNewspaperAsync(int newspaperId);
        Task<IEnumerable<Newspaper>> GetAllNewspapersAsync();
        Task<Newspaper?> GetNewspaperByIdAsync(int id);
        Task UpdateNewspaperAsync(Newspaper newspaper);
    }
}