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
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        public IssueService(IIssueRepository issueRepository)
        {
            _issueRepository = issueRepository;
        }
        public async Task<IEnumerable<Issue>> GetAllIssuesAsync()
        {
            return await _issueRepository.GetAllAsync(includeProperties: "Newspaper");
        }

        public async Task CreateIssueAsync(Issue issue)
        {
            ArgumentNullException.ThrowIfNull(issue);

            await _issueRepository.AddAsync(issue);
        }
        public async Task<bool> DeleteIssueAsync(int issueId)
        {
            try
            {
                var issue = await _issueRepository.GetAsync(i => i.IssueId == issueId);

                if (issue != null)
                {
                    await _issueRepository.DeleteAsync(issue);
                    return true;
                }
                else
                {
                    throw new InvalidOperationException($"Issue with ID {issueId} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
        public async Task<Issue?> GetIssueByIdAsync(int id)
        {
            return await _issueRepository.GetAsync(a => a.IssueId == id, includeProperties: "Newspaper,Advertisements");
        }   

        public async Task UpdateIssueAsync(Issue issue)
        {
            ArgumentNullException.ThrowIfNull(issue);

            await _issueRepository.UpdateAsync(issue);
        }
        public async Task<decimal> GetTotalCostAsync(int issueId)
        {
            return await _issueRepository.GetAdvertisementsCostAsync(issueId);
        }

        public async Task<decimal> GetNetProfitAsync(int issueId)
        {
            return await _issueRepository.GetNetProfitAsync(issueId);
        }
    }
}
