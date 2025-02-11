using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Domain.Entities;
using Editoria.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Infrastructure.Repository
{
    public class IssueRepository : Repository<Issue>, IIssueRepository
    {
        private readonly ApplicationDbContext _db;
        public IssueRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public async Task<decimal> GetAdvertisementsCostAsync(int issueId)
        {
            var result = await _db.Issues
               .Where(i => i.IssueId == issueId)
               .Select(i => _db.GetAdvertisementsCost(i.IssueId))
               .FirstOrDefaultAsync();

            return result;
        }
    }
}
