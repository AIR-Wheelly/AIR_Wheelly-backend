using AIR_Wheelly_Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIR_Wheelly_Common.Interfaces.Repository
{
    public interface ICarListingRepository : IRepository<CarListing>
    {
        Task<IEnumerable<CarListing>> GetCarListingsWithDetailsAsync();
        Task<CarListing?> GetCarListingWithDetailsAsync(Guid id);
        Task<CarListing?> GetCarListingByUserIdAsync(Guid userId);


    }
}
