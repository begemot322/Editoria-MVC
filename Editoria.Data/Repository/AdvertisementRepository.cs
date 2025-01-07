using Editoria.Data.Context;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Data.Repository
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly ApplicationDbContext _db;

        public AdvertisementRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AddAdvertisement(Advertisement advertisement)
        {
            _db.Advertisements.Add(advertisement);
            _db.SaveChanges();
        }

        public void DeleteAdvertisement(int advertisementId)
        {
            var advertisement = _db.Advertisements.FirstOrDefault(a => a.AdvertisementId == advertisementId);
            if (advertisement != null)
            {
                _db.Advertisements.Remove(advertisement);
                _db.SaveChanges();
            }
        }

        public Advertisement GetAdvertisementById(int? advertisementId)
        {
            return _db.Advertisements.FirstOrDefault(a => a.AdvertisementId == advertisementId);
        }

        public Advertisement GetAdvertisementWithIssue(int advertisementId)
        {
            return _db.Advertisements.Include(a => a.Issue).FirstOrDefault(a => a.AdvertisementId == advertisementId);
        }

        public IEnumerable<Advertisement> GetFilteredAdvertisements(string typeFilter, int? issueFilter)
        {
            var advertisements = _db.Advertisements.Include(a => a.Issue).AsQueryable();

            if (!string.IsNullOrEmpty(typeFilter))
            {
                advertisements = advertisements.Where(a => a.Type.Contains(typeFilter));
            }

            if (issueFilter.HasValue)
            {
                advertisements = advertisements.Where(a => a.IssueId == issueFilter.Value);
            }

            return advertisements.ToList();
        }

        public List<SelectListItem> GetIssueSelectList()
        {
            return _db.Issues.Select(n => new SelectListItem
            {
                Text = $"Номер выпуска: {n.IssueId} - {n.PublicationDate.ToShortDateString()}",
                Value = n.IssueId.ToString()
            }).ToList();
        }

        public void UpdateAdvertisement(Advertisement advertisement)
        {
            _db.Advertisements.Update(advertisement);
            _db.SaveChanges();
        }
    }
}