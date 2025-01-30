using Course_Work_Editoria.Authentication.Interfaces;
using Course_Work_Editoria.Services.File;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Course_Work_Editoria.Services.Auth
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly FileService _fileService;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository,
            IJwtProvider jwtProvider, FileService fileService)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _fileService = fileService;
        }

        public void Register(string userName, string email, string password, string phoneNumber, string role, IFormFile? imageFile)
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

            _userRepository.Add(user);
        }

        public string Login(string email, string password)
        {
            var user = _userRepository.GetByEmail(email);

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
        public User GetUserById(Guid userId)
        {
            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                throw new Exception("Пользователь не найден.");
            }

            return user;
        }
        public List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "User", Text = "User" },
                new SelectListItem { Value = "Moderator", Text = "Moderator" },
                new SelectListItem { Value = "Admin", Text = "Admin" },
            };
        }
    }
}
