namespace Editoria.Application.Services.Interfaces
{
    public interface IProfileService
    {
        Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword);
        Task UpdateUserImageAsync(Guid userId, string? imageUrl);
    }
}