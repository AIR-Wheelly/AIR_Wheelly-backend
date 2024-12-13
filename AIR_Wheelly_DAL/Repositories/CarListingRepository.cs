using AIR_Wheelly_Common.Interfaces.Repository;
using AIR_Wheelly_Common.Models;
using AIR_Wheelly_DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_DAL.Repositories
{
    public class CarListingRepository : Repository<CarListing>, ICarListingRepository
    {
        public CarListingRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CarListing>> GetCarListingsWithDetailsAsync()
        {
            return await _dbSet.Include(c => c.Model).ThenInclude(m => m.Manafacturer)
            .Where(cl => cl.IsActive).ToListAsync();
        }

        public async Task<CarListing?> GetCarListingWithDetailsAsync(Guid id)
        {
            return await _dbSet.Include(c => c.Model)
                         .ThenInclude(m => m.Manafacturer)
                         .Where(cl => cl.IsActive)
                         .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
