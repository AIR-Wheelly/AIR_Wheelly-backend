using AIR_Wheelly_Common.Interfaces;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;
using AIR_Wheelly_Common.Models;

namespace AIR_Wheelly_DAL.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(ApplicationDbContext context) : base(context) { }
        
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Id.ToString() == id);
        }
    }
}
