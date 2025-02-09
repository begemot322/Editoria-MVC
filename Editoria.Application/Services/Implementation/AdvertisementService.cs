using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Services;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Services.Implementation
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _advertisementRepository;
        public AdvertisementService(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }

        public async Task CreateAdvertisementAsync(Advertisement advertisement)
        {
            ArgumentNullException.ThrowIfNull(advertisement);

            await _advertisementRepository.AddAsync(advertisement);
        }
        public async Task<bool> DeleteAdvertisementAsync(int advertisementId)
        {
            try
            {
                var advertisement = await _advertisementRepository.GetAsync(a => a.AdvertisementId == advertisementId);

                if (advertisement != null)
                {
                    await _advertisementRepository.DeleteAsync(advertisement);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Advertisement with ID {advertisementId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisementsAsync(string typeFilter, int? issueFilter)
        {
            if (!string.IsNullOrEmpty(typeFilter) && issueFilter.HasValue)
            {
                return await _advertisementRepository.GetAllAsync(
                    u => u.Type.Contains(typeFilter) && u.IssueId == issueFilter.Value,
                    includeProperties: "Issue");
            }
            else
            {
                if (!string.IsNullOrEmpty(typeFilter))
                {
                    return await _advertisementRepository.GetAllAsync(
                        u => u.Type.Contains(typeFilter),
                        includeProperties: "Issue");
                }
                if (issueFilter.HasValue)
                {
                    return await _advertisementRepository.GetAllAsync(
                        u => u.IssueId == issueFilter.Value,
                        includeProperties: "Issue");
                }
            }
            return await _advertisementRepository.GetAllAsync(includeProperties: "Issue");
        }

        public async Task<Advertisement?> GetAdvertisementByIdAsync(int id)
        {
            return await _advertisementRepository.GetAsync(u => u.AdvertisementId == id);
        }

        public async Task UpdateAdvertisementAsync(Advertisement advertisement)
        {
            ArgumentNullException.ThrowIfNull(advertisement);

            await _advertisementRepository.UpdateAsync(advertisement);
        }
    }
}
