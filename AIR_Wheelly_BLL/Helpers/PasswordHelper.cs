using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_BLL.Helpers {
    public class PasswordHelper : IPasswordHelper {
        public string HashPassword(User user, string password) {
            throw new NotImplementedException();
        }

        public bool VerifyPassword(User user, string hashedPassword, string password) {
            throw new NotImplementedException();
        }
    }
}
