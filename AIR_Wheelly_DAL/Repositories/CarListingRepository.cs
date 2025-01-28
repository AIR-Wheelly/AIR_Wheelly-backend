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
            return await _dbSet.Include(c => c.Model)
                .ThenInclude(m => m.Manafacturer)
                .Include(l => l.Location)
                .Where(cl => cl.IsActive)
                .ToListAsync();
        }

        public async Task<CarListing?> GetCarListingWithDetailsAsync(Guid id)
        {
            var carListing = await _context.CarListings
                .Include(cl => cl.Model)
                .Include(cl => cl.Model.Manafacturer)
                .Include(cl => cl.Location)
                .Include(cl => cl.CarListingPictures)
                .Where(cl => cl.Id == id)
                .FirstOrDefaultAsync();

            if (carListing == null)
                return null;

            carListing.Model.ManafacturerName = (await _context.Manafacturers
                .FindAsync(carListing?.Model.ManafacturerId))?.Name;

            return carListing;
           
        }
       
        public async Task<CarListing?> GetCarListingById(Guid id)
        {
            return await _dbSet
                .Where(c => c.Id == id && c.IsActive) 
                .FirstOrDefaultAsync();
        }
        public async Task<List<CarListing>> GetListingsForOwnerAsync(Guid ownerId)
        {
            var carListings = await _context.CarListings
                .Include(cl => cl.Model) 
                .Include(cl => cl.Model.Manafacturer)
                .Include(cl => cl.Location) 
                .Include(cl => cl.CarListingPictures) 
                .Where(cl => cl.Id == ownerId)
                .ToListAsync();

            foreach (var carListing in carListings)
            {
                carListing.Model.ManafacturerName = (await _context.Manafacturers
                    .FindAsync(carListing.Model.ManafacturerId))?.Name;
            }

            return carListings;
        }


    }
}
