using Course_Work_Editoria.Authentication.Interfaces;
using Editoria.Data.Repository.IRepository;
using Editoria.Models;
using Editoria.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace Course_Work_Editoria.Authentication.Services
{
    public class UserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository,
            IJwtProvider jwtProvider, IWebHostEnvironment webHostEnvironment)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        public void Register(string userName, string email, string password, string phoneNumber,string role, IFormFile? imageFile)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            string? imageUrl = null;

            if (imageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

                string ImageFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "users");

                string filePath = Path.Combine(ImageFolder, fileName);

                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                imageUrl = $"/images/users/{fileName}";
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
            var user =  _userRepository.GetByEmail(email);

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

            if(isPasswordValid == false)
            {
                throw new Exception("Неверный текущий пароль.");
            }

            var newHashedPassword = _passwordHasher.Generate(newPassword);

            user.PasswordHash = newHashedPassword;

            _userRepository.Update(user);
        }
        public List<SelectListItem> GetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "User", Text = "User" },
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Moderator", Text = "Moderator" }
            };
        }
    }
}
