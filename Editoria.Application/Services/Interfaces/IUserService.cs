using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Editoria.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid userId);
        Task<string> LoginAsync(string email, string password);
        Task RegisterAsync(string userName, string email, string password, string phoneNumber, string role, IFormFile? imageFile);
    }
}