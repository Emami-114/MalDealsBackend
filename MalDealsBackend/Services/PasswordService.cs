using MalDealsBackend.Models.Entitys;
using Microsoft.AspNetCore.Identity;

namespace MalDealsBackend.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<object> _hasher = new();

        public string HashPassword(UserEntity user, string password)
        {
            return _hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(UserEntity user, string hashedPassword, string inputPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, inputPassword);
            return result == PasswordVerificationResult.Success;
        }
    }
}