using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_Common.Interfaces.Repository;

namespace AIR_Wheelly_DAL.Repositories
{
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

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
