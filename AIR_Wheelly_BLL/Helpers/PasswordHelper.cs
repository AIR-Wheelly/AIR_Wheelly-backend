using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Helpers {
    public class PasswordHelper : IPasswordHelper {
        public string HashPassword(User user, string password) {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string password) {
            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, hashedPassword, password);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
