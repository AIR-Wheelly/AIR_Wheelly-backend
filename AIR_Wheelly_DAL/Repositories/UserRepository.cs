using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Data;
using AIR_Wheelly_DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
