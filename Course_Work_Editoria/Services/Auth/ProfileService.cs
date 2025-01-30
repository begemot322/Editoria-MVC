using Course_Work_Editoria.Authentication.Interfaces;
using Course_Work_Editoria.Services.File;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace Course_Work_Editoria.Services.Auth
{
    public class ProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMemoryCache _memoryCache;
        private readonly FileService _fileService;

        public ProfileService(IUserRepository userRepository, IPasswordHasher passwordHasher,
            IMemoryCache memoryCache, FileService fileService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _memoryCache = memoryCache;
            _fileService = fileService;
        }

        public void ChangePassword(Guid userId, string currentPassword, string newPassword, string confirmPassword)
        {
            if (newPassword != confirmPassword)
            {
                throw new Exception("Ваши пароли не совпадают.");
            }

            var user = _userRepository.GetById(userId);

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

            _userRepository.Update(user);
        }

        public void UpdateUserImage(Guid userId, string? imageUrl)
        {
            var user = _userRepository.GetById(userId);
            if (user != null && imageUrl != null)
            {
                if (!string.IsNullOrEmpty(user.ImageUrl))
                {
                    _fileService.DeleteFile(user.ImageUrl);
                }


                user.ImageUrl = imageUrl;
                _userRepository.Update(user);

                _memoryCache.Remove($"User_{userId}");
            }
        }
    }
}
