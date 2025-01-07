using Editoria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository.IRepository
{
    public interface IAdvertisementRepository
    {
        IEnumerable<Advertisement> GetFilteredAdvertisements(string typeFilter, int? issueFilter);
        List<SelectListItem> GetIssueSelectList();
        Advertisement GetAdvertisementById(int? advertisementId);
        void AddAdvertisement(Advertisement advertisement);
        void UpdateAdvertisement(Advertisement advertisement);
        void DeleteAdvertisement(int advertisementId);
        Advertisement GetAdvertisementWithIssue(int advertisementId);

    }
}
