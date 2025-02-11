using Editoria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Editoria.Application.Common.Interfaces.Repositories
{
    public interface IIssueRepository : IRepository<Issue>
    {
        Task<decimal> GetAdvertisementsCostAsync(int issueId);
    }
}
