using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_Common.Interfaces {
    public interface IPasswordHelper {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string password);
    }
}
