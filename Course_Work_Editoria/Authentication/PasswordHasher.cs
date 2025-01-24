﻿using Course_Work_Editoria.Authentication.Interfaces;

namespace Course_Work_Editoria.Authentication
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
