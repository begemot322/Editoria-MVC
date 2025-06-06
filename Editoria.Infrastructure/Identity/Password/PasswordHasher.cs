﻿using Editoria.Application.Common.Interfaces.Identity;

namespace Editoria.Infrastructure.Identity.Password
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Generate(string password)
        {
            return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        }

        public bool Verify(string password, string HashPassword)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, HashPassword);
        }
    }
}
