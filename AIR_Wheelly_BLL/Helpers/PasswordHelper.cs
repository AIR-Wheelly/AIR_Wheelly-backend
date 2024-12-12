using AIR_Wheelly_Common.Interfaces;
using Microsoft.AspNetCore.Identity;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_BLL.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }

        public static bool VerifyPassword(User user, string hashedPassword, string password)
        {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, hashedPassword, password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
