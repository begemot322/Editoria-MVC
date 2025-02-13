using Editoria.Domain.Entities;

namespace Editoria.Application.Services.Services
{
    public interface IIssueService
    {
        Task CreateIssueAsync(Issue issue);
        Task<bool> DeleteIssueAsync(int issueId);
        Task<IEnumerable<Issue>> GetAllIssuesAsync();
        Task<Issue?> GetIssueByIdAsync(int id);
        Task UpdateIssueAsync(Issue issue);
        Task<decimal> GetTotalCostAsync(int id);
        Task<decimal> GetNetProfitAsync(int id);
    }
}