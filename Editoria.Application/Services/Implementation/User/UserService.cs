using Course_Work_Editoria.Services.File;
using Editoria.Application.Common.Interfaces.Identity;
using Editoria.Application.Common.Interfaces.Repositories;
using Editoria.Application.Services.Interfaces;
using Editoria.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Course_Work_Editoria.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IFileService _fileService;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository,
            IJwtProvider jwtProvider, IFileService fileService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _fileService = fileService;
        }

        public async Task RegisterAsync(string userName, string email, string password, string phoneNumber, string role, IFormFile? imageFile)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            string? imageUrl = null;

            if (imageFile != null)
            {
                imageUrl = _fileService.SaveFile(imageFile, "images/users");
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                Email = email,
                PasswordHash = hashedPassword,
                PhoneNumber = phoneNumber,
                ImageUrl = imageUrl,
                Role = role,
            };

            await _userRepository.AddAsync(user);
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(u => u.Email == email);

            if (user == null)
            {
                throw new Exception("Неверный логин или пароль.");
            }

            var result = _passwordHasher.Verify(password, user.PasswordHash);


            if (result == false)
            {
                throw new Exception("Неверный логин или пароль");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
        public async Task<User> GetUserByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new Exception("Пользователь не найден.");
            }

            return user;
        }
    }
}
