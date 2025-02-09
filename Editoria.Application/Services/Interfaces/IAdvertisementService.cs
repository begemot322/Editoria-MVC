using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface IAdvertisementService
    {
        Task CreateAdvertisementAsync(Advertisement advertisement);
        Task<bool> DeleteAdvertisementAsync(int advertisementId);
        Task<Advertisement?> GetAdvertisementByIdAsync(int id);
        Task<IEnumerable<Advertisement>> GetAllAdvertisementsAsync(string typeFilter, int? issueFilter);
        Task UpdateAdvertisementAsync(Advertisement advertisement);
    }
}