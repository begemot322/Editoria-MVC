using Course_Work_Editoria.Services.File;
using Editoria.Application.Common.Interfaces.Identity;
using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Common.Interfaces.Services;
using Editoria.Application.Services.Interfaces;
namespace Course_Work_Editoria.Services.Auth

{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ICacheService _cacheService;
        private readonly IFileService _fileService;

        public ProfileService(IUserRepository userRepository, IPasswordHasher passwordHasher,
            ICacheService cacheService, IFileService fileService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _cacheService = cacheService;
            _fileService = fileService;
        }

        public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                throw new Exception("Ваши пароли не совпадают.");
            }

            var user = await _userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("Пользователь не найден.");
            }

            var isPasswordValid = _passwordHasher.Verify(currentPassword, user.PasswordHash);

            if (isPasswordValid == false)
            {
                throw new Exception("Неверный текущий пароль.");
            }

            var newHashedPassword = _passwordHasher.Generate(newPassword);
            user.PasswordHash = newHashedPassword;

            await _userRepository.UpdateAsync(user);
        }

        public async Task UpdateUserImageAsync(Guid userId, string? imageUrl)
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId);
            if (user != null && imageUrl != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    _fileService.DeleteFile(user.ImageUrl);
                }


                user.ImageUrl = imageUrl;
                await _userRepository.UpdateAsync(user);

                _cacheService.Remove($"User_{userId}");
            }
        }
    }
}
