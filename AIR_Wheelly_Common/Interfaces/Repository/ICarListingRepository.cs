using AIR_Wheelly_Common.Models;


namespace AIR_Wheelly_Common.Interfaces.Repository
{
    public interface ICarListingRepository : IRepository<CarListing>
    {
        Task<IEnumerable<CarListing>> GetCarListingsWithDetailsAsync();
        Task<CarListing?> GetCarListingWithDetailsAsync(Guid id);
        Task<CarListing?> GetCarListingById(Guid id);
    }
}
