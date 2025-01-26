using Course_Work_Editoria.Authentication.Interfaces;
using Editoria.Data.Repository.IRepository;
using Editoria.Models.Entities;

namespace Course_Work_Editoria.Authentication.Services
{
    public class ProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ProfileService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
    }
}
